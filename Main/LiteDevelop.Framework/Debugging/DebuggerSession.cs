﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using LiteDevelop.Framework.FileSystem;

namespace LiteDevelop.Framework.Debugging
{
    public abstract class DebuggerSession : IDisposable
    {
        public DebuggerSession()
        {
            ProgressReporter = EmptyProgressReporter.Instance;
        }

        public event EventHandler ActiveChanged;
        public event EventHandler Disposed;
        public event PauseEventHandler Paused;
        public event EventHandler Resumed;
        
        private bool _isActive;

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    OnActiveChanged(EventArgs.Empty);
                }
            }
        }

        public IProgressReporter ProgressReporter
        {
            get;
            set;
        }

        public abstract bool CanBreak
        {
            get;
        }

        public abstract bool CanStepOver
        {
            get;
        }

        public abstract bool CanStepInto
        {
            get;
        }

        public abstract bool CanStepOut
        {
            get;
        }

        public abstract bool CanContinue
        {
            get;
        }

        public abstract SourceRange CurrentSourceRange
        {
            get;
        }

        public abstract void Start(ProcessStartInfo startInfo);

        public abstract void Attach(Process process);

        public abstract void Detach();

        public abstract void BreakAll();

        public abstract void StopAll();

        public abstract void Continue();

        public abstract void StepOver();

        public abstract void StepInto();

        public abstract void StepOut();

        protected virtual void OnActiveChanged(EventArgs e)
        {
            if (ActiveChanged != null)
                ActiveChanged(this, e);
        }

        protected virtual void OnPaused(PauseEventArgs e)
        {
            if (Paused != null)
                Paused(this, e);
        }

        protected virtual void OnResumed(EventArgs e)
        {   
            if (Resumed != null)
                Resumed(this, e);
        }

        public abstract void Dispose(bool disposing);

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            if (Disposed != null)
                Disposed(this, EventArgs.Empty);
        }

        #endregion
    }
}
