import React, { Component } from "react";
import axios from 'axios'

class Login extends Component {
  //初始化状态
  state = {
    username: "", //用户名
    password: "", //密码
  };

  //保存用户名到状态中
  saveUsername = (event) => {
    this.setState({ username: event.target.value });
  };

  //保存密码到状态中
  savePassword = (event) => {
    this.setState({ password: event.target.value });
  };

  //表单提交的回调
  handleSubmit = (event) => {
     event.preventDefault(); //阻止表单提交
     const { username, password } = this.state;
     alert(`你输入的用户名是：${username},你输入的密码是：${password}`);
    axios.get(`https://localhost:5001/api/User/login/${this.state.username}/${this.state.password}`).then(
        response => {
            //请求成功后通知App更新状态
            // this.props.updateAppState({isLoading:false,users:response.data.items})
            alert(response.data.items);
        },
        error => {
            //请求失败后通知App更新状态
            // this.props.updateAppState({isLoading:false,err:error.message})
            alert(error.message);

        }
    )
  };

  render() {
    return (
      <div className="text-center login-body">
        <form className="form-signin" onSubmit={this.handleSubmit}>
          <img
            className="mb-4"
            src="https://getbootstrap.com/docs/4.0/assets/brand/bootstrap-solid.svg"
            alt=""
            width="72"
            height="72"
          />
          <h1 className="h3 mb-3 font-weight-normal">Please sign in</h1>
          <label for="inputEmail" className="sr-only">
            Email address
          </label>
          <input
            type="email"
            id="inputEmail"
            className="form-control"
            placeholder="Email address"
            onChange={this.saveUsername}
            required=""
            autofocus=""
          />
          <label for="inputPassword" className="sr-only">
            Password
          </label>
          <input
            type="password"
            id="inputPassword"
            className="form-control"
            placeholder="Password"
            onChange={this.savePassword}
            required=""
          />
          <div className="checkbox mb-3">
            <label>
              <input type="checkbox" value="remember-me" /> Remember me
            </label>
          </div>
          <button className="btn btn-lg btn-primary btn-block" type="submit">
            Sign in
          </button>
          {/* <p className="mt-5 mb-3 text-muted">© 2017-2018</p> */}
        </form>
      </div>
    );
  }
}

export default Login;
