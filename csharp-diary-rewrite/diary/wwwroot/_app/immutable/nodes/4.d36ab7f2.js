import{s as b,n as x,r as S}from"../chunks/scheduler.e108d1fd.js";import{S as w,i as C,g as d,s as T,h,j as E,c as g,f as _,y as R,k as u,a as A,x as p,z as m,A as v}from"../chunks/index.c5af3f48.js";import{S as N}from"../chunks/Constants.35362079.js";function O(i){let e,s,r,t,c,n,f="Save",o,y;return{c(){e=d("div"),s=d("input"),r=T(),t=d("textarea"),c=T(),n=d("button"),n.textContent=f,this.h()},l(l){e=h(l,"DIV",{class:!0});var a=E(e);s=h(a,"INPUT",{type:!0,placeholder:!0,class:!0}),r=g(a),t=h(a,"TEXTAREA",{class:!0,placeholder:!0}),E(t).forEach(_),c=g(a),n=h(a,"BUTTON",{class:!0,"data-svelte-h":!0}),R(n)!=="svelte-wecwci"&&(n.textContent=f),a.forEach(_),this.h()},h(){u(s,"type","text"),u(s,"placeholder","Title"),u(s,"class","input input-bordered w-full"),u(t,"class","textarea textarea-bordered resize-none w-full h-4/5"),u(t,"placeholder","Bio"),u(n,"class","btn btn-success w-full"),u(e,"class","flex flex-col items-center h-full gap-3 mt-4 p-4")},m(l,a){A(l,e,a),p(e,s),m(s,i[0]),p(e,r),p(e,t),m(t,i[1]),p(e,c),p(e,n),o||(y=[v(s,"input",i[3]),v(t,"input",i[4]),v(n,"click",i[2])],o=!0)},p(l,[a]){a&1&&s.value!==l[0]&&m(s,l[0]),a&2&&m(t,l[1])},i:x,o:x,d(l){l&&_(e),o=!1,S(y)}}}function P(i,e,s){let r="",t="";async function c(){let o=await fetch(N+"/entries",{method:"POST",headers:{"Content-Type":"application/json"},body:JSON.stringify({title:r,text:t})});o.status==200?console.log("Entry saved successfully"):console.log("Entry save failed"+o.status+" "+o.statusText)}function n(){r=this.value,s(0,r)}function f(){t=this.value,s(1,t)}return[r,t,c,n,f]}class z extends w{constructor(e){super(),C(this,e,P,O,b,{})}}export{z as component};