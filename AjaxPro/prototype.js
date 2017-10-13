//特别注意：本文件内容应与LFFramework.Commonjs.prototype.js保持同步一致，切记！！
//2009-9-19吕开余
var isIE = (document.all) ? true : false;
//获取对象类型，可跨页面
function getType(o)
{
  var cls=Object.prototype.toString.call(o); //结果如："[object Array]"
  var type=cls.substring(8,cls.indexOf("]"));
  return type;
}
//获取页面元素的缩简函数
var $ = function (id) {
	return getType(id)=="String"? document.getElementById(id) : id;
};
//对事件参数对象进行扩展封装,实现兼容
function toEvent(e){
	var oEvent = isIE ? window.event : e;
	if (isIE) {
		oEvent.pageX = oEvent.clientX + document.documentElement.scrollLeft;
		oEvent.pageY = oEvent.clientY + document.documentElement.scrollTop;
		oEvent.preventDefault = function () { this.returnValue = false; };
		oEvent.detail = oEvent.wheelDelta / (-40);
		oEvent.stopPropagation = function(){ this.cancelBubble = true; }; 
	}
	return oEvent;
}
//获取元素的当前样式
function currentStyle(element){
	return element.currentStyle || document.defaultView.getComputedStyle(element, null);
}

//用于生成采用初始化函数构造对象的类
var Class = {
	create: function() {
		return function() { this.initialize.apply(this, arguments); }
	}
}
Object.extend = function(dest, source, replace) {
	for(var prop in source) {
		if(replace == false && dest[prop] != null) { continue; }
		dest[prop] = source[prop];
	}
	return dest;
};

Object.extend(Function.prototype, { 
	apply: function(o, a) {
		var r, x = "__fapply";
		if(typeof o != "object") { o = {}; }
		o[x] = this;
		var s = "r = o." + x + "(";
		for(var i=0; i<a.length; i++) {
			if(i>0) { s += ","; }
			s += "a[" + i + "]";
		}
		s += ");";
		eval(s);
		delete o[x];
		return r;
	},
	bind: function(o) {
	    //把调用bind时传的参数传给被调函数
	    var bindArgs = Array.prototype.slice.call(arguments,1);
	    var fun=this;
	    return function() {
	        //把回调经bind后生成函数时的参数加在bind时传的参数后，一同传给被调函数
	        //须用这种方式调用，因为arguments类型检查为Object，不能直接加到数组
	        var args=bindArgs.concat(Array.prototype.slice.call(arguments,0));  
		    return fun.apply(o, args);
		};
	},
	bindAsEventHandler:function(object) {
	    var fun=this;
	    return function(e) {
			return fun.call(object,this,toEvent(e));
		}
	}
}, false);

Object.extend(Array.prototype, {
	push: function(o) {
		this[this.length] = o;
	},
	addRange: function(items) {
		if(items.length > 0) {
			for(var i=0; i<items.length; i++) {
				this.push(items[i]);
			}
		}
	},
	clear: function() {
		this.length = 0;
		return this;
	},
	shift: function() {
		if(this.length == 0) { return null; }
		var o = this[0];
		for(var i=0; i<this.length-1; i++) {
			this[i] = this[i + 1];
		}
		this.length--;
		return o;
	}
}, false);

Object.extend(String.prototype, {
	trimLeft: function() {
		return this.replace(/^\s*/,"");
	},
	trimRight: function() {
		return this.replace(/\s*$/,"");
	},
	trim: function() {
		return this.trimRight().trimLeft();
	},
	endsWith: function(s) {
		if(this.length == 0 || this.length < s.length) { return false; }
		return (this.substr(this.length - s.length) == s);
	},
	startsWith: function(s) {
		if(this.length == 0 || this.length < s.length) { return false; }
		return (this.substr(0, s.length) == s);
	},
	split: function(c) {
		var a = [];
		if(this.length == 0) return a;
		var p = 0;
		for(var i=0; i<this.length; i++) {
			if(this.charAt(i) == c) {
				a.push(this.substring(p, i));
				p = ++i;
			}
		}
		a.push(s.substr(p));
		return a;
	}
}, false);

Object.extend(String, {
	format: function(s) {
		for(var i=1; i<arguments.length; i++) {
			s = s.replace(new RegExp("\\{" + (i -1) + "\\}","g"), arguments[i]);
		}
		return s;
	},
	isNullOrEmpty: function(s) {
		if(s == null || s.length == 0) {
			return true;
		}
		return false;
	}
}, false);

if(typeof addEvent == "undefined")
	addEvent = function(o, evType, f, capture) {
		if(o == null) { return false; }
		if(o.addEventListener) {
			o.addEventListener(evType, f, capture);
			return true;
		} else if (o.attachEvent) {
			var r = o.attachEvent("on" + evType, f);
			return r;
		} else {
			try{ o["on" + evType] = f; }catch(e){}
		}
	};
	
if(typeof removeEvent == "undefined")
	removeEvent = function(o, evType, f, capture) {
		if(o == null) { return false; }
		if(o.removeEventListener) {
			o.removeEventListener(evType, f, capture);
			return true;
		} else if (o.detachEvent) {
			o.detachEvent("on" + evType, f);
		} else {
			try{ o["on" + evType] = function(){}; }catch(e){}
		}
	};

