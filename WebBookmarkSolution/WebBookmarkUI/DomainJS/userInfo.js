﻿$(document).ready(function () {// DOM的onload事件处理函数  
    $('#btnSaveUserInfo').bind('click', function (e) {
        e.preventDefault();
        checkEmail();
        e.stopPropagation();
    });

    $('#uploadFile').fileupload({
        url: userImgUploadUrl,
        dataType: 'json',
        done: function (e, rsp) {
            if (rsp.result.IsSuccess) {
                alert(rsp.result.SuccessMessage);

            } else {
                alert(rsp.result.ErrorMessage);
            }
            location.reload();
        } 
    });



});

function checkEmail()
{
    var email = $("#user-email").val();
    if (email == "") {
        alert("请输入邮箱地址。");
        return;
    }
    $.ajax({
        type: "post",
        url: "./UserInfo/CheckUserEmail",
        data: { email},
        success:
            function (rsp) {
                if (rsp.IsSuccess)
                {
                    checkLoginName();
                }
                else
                {
                    alert(rsp.ErrorMessage);
                    return;
                }  
            }
    });
}

function checkLoginName()
{
    var loginName = $("#login-name").val();
    if (loginName == "") {
        alert("请输入登陆名呀。");
        return;
    }

    $.ajax({
        type: "post",
        url: "./UserInfo/CheckUserLoginName",
        data: { loginName},
                success:
            function (rsp) {
                if (rsp.IsSuccess)
                {
                    saveUserInfoToDB();
                }
                else {
                    alert(rsp.ErrorMessage);
                    return;
                }
            }
    });
}

function saveUserInfoToDB()
{
    var email = $("#user-email").val();

    var loginName = $("#login-name").val();

    var name = $("#user-name").val();
    
    var phone = $("#user-phone").val();

    var QQ = $("#user-QQ").val();

    var usercomment = $("#user-intro").val();

    var uiUserInfo =
   {
       UserEmail: email,
       UserName: name,
       Phone: phone,
       QQ: QQ,
       UserInfoComment: usercomment,
       LoginName: loginName
   };


    $.ajax({
        type: "post",
        url: "./UserInfo/SaveUserInfo",
        data: uiUserInfo,
        success:
            function (rsp) {
                if (rsp.IsSuccess) {
                    alert(rsp.SuccessMessage);
                    
                }
                else
                    alert(rsp.ErrorMessage);
            }
    });



}