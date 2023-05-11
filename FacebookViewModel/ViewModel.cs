﻿using Common.Contracts;
using FacebookModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//using FacebookWrapper.ObjectModel;

namespace FacebookViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        public static LoginService m_LoginService;

        private IFacebookUser m_FacebookUser;

        //private ObservableCollection<PostAdapter> m_posts;
        public DateTime m_AppStarTime { get; set; }
        public TimeWellSpend m_TimeWellSpend { get; set; }

        public BindingSource m_BindingSourcePosts;
        private string m_AccessToken;
        public string AccessToken
        {
            get => m_AccessToken;
            set
            {
                if(m_AccessToken != value)
                {
                    m_AccessToken = value;
                    OnPropertyChanged(nameof(AccessToken));
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public LoginService LoginService
        {
            get => m_LoginService;
            set
            {
                if (m_LoginService != value)
                {
                    m_LoginService = value;
                    OnPropertyChanged(nameof(LoginService));
                }
            }
        }

        public IFacebookUser FacebookUser
        {
            get => m_FacebookUser;
            set
            {
                if(m_FacebookUser != value)
                {
                    m_FacebookUser = value;
                    OnPropertyChanged(nameof(FacebookUser));
                }
            }
        }

        //public ObservableCollection<PostAdapter> Posts
        //{
        //    get => m_posts; set => SetField(ref m_posts, value);
        //}

        public BindingSource BindingSourcePosts
        {
            get => m_BindingSourcePosts;
            set
            {
                if (m_BindingSourcePosts != value)
                {
                    m_BindingSourcePosts = value;
                    OnPropertyChanged(nameof(BindingSourcePosts));
                }
            }
        }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        //protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        //{
        //    if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        //    field = value;
        //    OnPropertyChanged(propertyName);
        //    return true;
        //}

        public void AutoLogin(string i_AccessToken)
        {
            try
            {
                m_LoginService = LoginService.Instance;
                m_LoginService.AutoLogin(i_AccessToken);
                m_FacebookUser = m_LoginService.m_LoginUser;
                doAfterLogin(m_FacebookUser);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void doAfterLogin(IFacebookUser i_LoginUser)
        {
            if (i_LoginUser != null)
            {
                AccessToken = m_LoginService.m_AccessToken;
                //m_FacebookUser.LoadPostsFromApi();
            }
        }

        public void LoginButtonClicked()
        {
            try
            {
                m_LoginService = LoginService.Instance;
                m_LoginService.LoginAndInit();
                m_FacebookUser = m_LoginService.m_LoginUser;
                doAfterLogin(m_FacebookUser);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            //m_LoginService.LoadPostsFromApi();
            //m_bsPosts = new BindingSource { DataSource = m_LoginService.Posts };

        }

        public void LogoutButtonClicked()
        {
            m_LoginService.LogoutAndSet();
        }



        public string SetTimersMSG()
        {
            DateTime now = DateTime.Now;
            TimeSpan timePast = now - m_AppStarTime;
            string appUsingTime = string.Format(
                @"{0} days,
{1} hours,
{2} minutes,
{3} seconds
of your life in our stupid App!",
                timePast.Days,
                timePast.Hours,
                timePast.Minutes,
                timePast.Seconds);
            return appUsingTime;
        }

        public string GetBetterThingFromDic()
        {
            if (m_TimeWellSpend == null)
            {
                m_TimeWellSpend = new TimeWellSpend();
            }
            string betterThingToDo = m_TimeWellSpend.GetActivity(m_AppStarTime);
            return betterThingToDo;
        }

    }
}
