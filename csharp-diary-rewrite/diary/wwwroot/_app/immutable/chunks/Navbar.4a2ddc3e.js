import{s as k,n as f,o as C}from"./scheduler.19f29636.js";import{S as y,i as T,g as h,s as N,h as d,j as O,y as _,c as S,f as m,k as u,a as p,x as b,z as x}from"./index.90f2793d.js";import{g}from"./navigation.df8e3220.js";import{J as v}from"./Constants.f94e8d01.js";function E(r){let t,e="login",s,l;return{c(){t=h("button"),t.textContent=e,this.h()},l(n){t=d(n,"BUTTON",{class:!0,"data-svelte-h":!0}),_(t)!=="svelte-1i2kei9"&&(t.textContent=e),this.h()},h(){u(t,"class","btn btn-success btn-sm")},m(n,c){p(n,t,c),s||(l=x(t,"click",r[2]),s=!0)},p:f,d(n){n&&m(t),s=!1,l()}}}function I(r){let t,e="logout",s,l;return{c(){t=h("button"),t.textContent=e,this.h()},l(n){t=d(n,"BUTTON",{class:!0,"data-svelte-h":!0}),_(t)!=="svelte-14by0b3"&&(t.textContent=e),this.h()},h(){u(t,"class","btn btn-warning btn-sm")},m(n,c){p(n,t,c),s||(l=x(t,"click",r[1]),s=!0)},p:f,d(n){n&&m(t),s=!1,l()}}}function $(r){let t,e,s="diary",l;function n(o,i){return o[0]?I:E}let c=n(r),a=c(r);return{c(){t=h("div"),e=h("a"),e.textContent=s,l=N(),a.c(),this.h()},l(o){t=d(o,"DIV",{class:!0});var i=O(t);e=d(i,"A",{href:!0,class:!0,"data-svelte-h":!0}),_(e)!=="svelte-j3e92z"&&(e.textContent=s),l=S(i),a.l(i),i.forEach(m),this.h()},h(){u(e,"href","/"),u(e,"class","normal-case text-xl"),u(t,"class","navbar bg-base-100 flex justify-between px-10")},m(o,i){p(o,t,i),b(t,e),b(t,l),a.m(t,null)},p(o,[i]){c===(c=n(o))&&a?a.p(o,i):(a.d(1),a=c(o),a&&(a.c(),a.m(t,null)))},i:f,o:f,d(o){o&&m(t),a.d()}}}function j(r,t,e){let s=!1;C(()=>{e(0,s=localStorage.getItem(v)!==null)});async function l(){localStorage.removeItem(v),g("/login")}return[s,l,()=>g("/login")]}class B extends y{constructor(t){super(),T(this,t,j,$,k,{})}}export{B as N};
