function cuong(){
	var stt="cuong pro";
	var b="<div>" + stt
             
              
      
                  
                  
                 + "</div>";
	var a=document.getElementById("logo").innerHTML=b;
	return a;
	
	           
}
var enter="<br />";
function Checkdangnhap(event, minKt, maxKt)

{

    event.preventDefault();

    var mess = document.getElementById('errorText');

    var username = document.getElementById('tendn').value;



    if (username == '')

    {

        document.getElementById('tendn').style.backgroundColor = 'yellow';

        mess.innerHTML += 'Tên đăng nhập ko được để trống ' + enter;

    } else if ((username.length > 0 && username.length < minKt) || username.length > maxKt)

    {

        document.getElementById('tendn').style.backgroundColor = 'yellow';

        mess.innerHTML += 'Tên đăng nhập từ 3-10 kí tự ' + enter;

    } else mess.innerHTML = 'Tên đăng nhập của bạn là :' + username + '' + enter;

}
function Checkpass(event,minKt, maxKt)

{

    event.preventDefault();
	

    var pass = document.getElementById('mk').value;

    var mess = document.getElementById('errorText');

    if (pass == '')

    {

        document.getElementById('mk').style.backgroundColor = 'yellow';

        mess.innerHTML += 'Mật khẩu ko được để trống ' + enter;

    }else if ((pass.length > 0 && pass.length < minKt) || pass.length > maxKt)
	{
		document.getElementById('mk').style.backgroundColor = 'yellow';
		mess.innerHTML += 'Mật khẩu phải  từ 5-15 kí tự ' + enter;
	}
	 else mess.innerHTML += 'Mật khẩu của bạn là ' + Array(pass.length + 1).join("*") + '' + enter;

}

function Checkemail(event)

{

    event.preventDefault();

    var mess = document.getElementById('errorText');

    var letter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

    var email = document.getElementById('email').value;

    if (email == '')

    {

        document.getElementById('email').style.backgroundColor = 'yellow';

        mess.innerHTML += 'Email ko được để trống ' + enter;

    } else if (!email.match(letter))

    {

        document.getElementById('email').style.backgroundColor = 'yellow';

        mess.innerHTML += 'Email nhập sai định dạng';

    } else mess.innerHTML += 'Email của bạn là :' + email + '' + enter;



}
var btndangnhap = document.getElementById('btnDangnhap');

function Validate() {

    //Mặc định các lỗi này sẽ ẩn

    var mess = document.getElementById('errorText');

    mess.innerHTML = '';

    //Gọi các hàm đã viết

    Checkdangnhap(event, 3, 10);
	
	
	
	Checkpass(event,5, 10);
	

    Checkemail(event);
	
	var b=document.getElementById('btnDangnhap');
	
	b.onclick=function(){
		 var username = document.getElementById('tendn').value;
		  var pass = document.getElementById('mk').value;
		   var email = document.getElementById('email').value;

		 if (username == '' )
		  alert("Đăng ký không thành cộng");
		 else if(pass=='')
		   alert("Đăng ký không thành cộng");
		 else if(email=='')
		   alert("Đăng ký không thành cộng");
		 
		
		else alert("Đăng ký thành cộng");
	}
	
	
	
	
	
	
}



                       
                        
                       
                        
                      
                      
                      
                      
                  
                  
                 
