﻿using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System
{
	// Token: 0x020000F8 RID: 248
	[ComVisible(false)]
	[DebuggerTypeProxy(typeof(System_LazyDebugView<>))]
	[DebuggerDisplay("ThreadSafetyMode={Mode}, IsValueCreated={IsValueCreated}, IsValueFaulted={IsValueFaulted}, Value={ValueForDebugDisplay}")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	[Serializable]
	public class Lazy<T>
	{
		// Token: 0x06000F12 RID: 3858 RVA: 0x0002EE0D File Offset: 0x0002D00D
		[__DynamicallyInvokable]
		public Lazy() : this(LazyThreadSafetyMode.ExecutionAndPublication)
		{
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0002EE16 File Offset: 0x0002D016
		[__DynamicallyInvokable]
		public Lazy(Func<T> valueFactory) : this(valueFactory, LazyThreadSafetyMode.ExecutionAndPublication)
		{
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0002EE20 File Offset: 0x0002D020
		[__DynamicallyInvokable]
		public Lazy(bool isThreadSafe) : this(isThreadSafe ? LazyThreadSafetyMode.ExecutionAndPublication : LazyThreadSafetyMode.None)
		{
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0002EE2F File Offset: 0x0002D02F
		[__DynamicallyInvokable]
		public Lazy(LazyThreadSafetyMode mode)
		{
			this.m_threadSafeObj = Lazy<T>.GetObjectFromMode(mode);
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0002EE43 File Offset: 0x0002D043
		[__DynamicallyInvokable]
		public Lazy(Func<T> valueFactory, bool isThreadSafe) : this(valueFactory, isThreadSafe ? LazyThreadSafetyMode.ExecutionAndPublication : LazyThreadSafetyMode.None)
		{
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0002EE53 File Offset: 0x0002D053
		[__DynamicallyInvokable]
		public Lazy(Func<T> valueFactory, LazyThreadSafetyMode mode)
		{
			if (valueFactory == null)
			{
				throw new ArgumentNullException("valueFactory");
			}
			this.m_threadSafeObj = Lazy<T>.GetObjectFromMode(mode);
			this.m_valueFactory = valueFactory;
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x0002EE7C File Offset: 0x0002D07C
		private static object GetObjectFromMode(LazyThreadSafetyMode mode)
		{
			if (mode == LazyThreadSafetyMode.ExecutionAndPublication)
			{
				return new object();
			}
			if (mode == LazyThreadSafetyMode.PublicationOnly)
			{
				return LazyHelpers.PUBLICATION_ONLY_SENTINEL;
			}
			if (mode != LazyThreadSafetyMode.None)
			{
				throw new ArgumentOutOfRangeException("mode", Environment.GetResourceString("Lazy_ctor_ModeInvalid"));
			}
			return null;
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0002EEAC File Offset: 0x0002D0AC
		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			T value = this.Value;
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0002EEC0 File Offset: 0x0002D0C0
		[__DynamicallyInvokable]
		public override string ToString()
		{
			if (!this.IsValueCreated)
			{
				return Environment.GetResourceString("Lazy_ToString_ValueNotCreated");
			}
			T value = this.Value;
			return value.ToString();
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000F1B RID: 3867 RVA: 0x0002EEF4 File Offset: 0x0002D0F4
		internal T ValueForDebugDisplay
		{
			get
			{
				if (!this.IsValueCreated)
				{
					return default(T);
				}
				return ((Lazy<T>.Boxed)this.m_boxed).m_value;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x0002EF23 File Offset: 0x0002D123
		internal LazyThreadSafetyMode Mode
		{
			get
			{
				if (this.m_threadSafeObj == null)
				{
					return LazyThreadSafetyMode.None;
				}
				if (this.m_threadSafeObj == LazyHelpers.PUBLICATION_ONLY_SENTINEL)
				{
					return LazyThreadSafetyMode.PublicationOnly;
				}
				return LazyThreadSafetyMode.ExecutionAndPublication;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x0002EF3F File Offset: 0x0002D13F
		internal bool IsValueFaulted
		{
			get
			{
				return this.m_boxed is Lazy<T>.LazyInternalExceptionHolder;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x0002EF4F File Offset: 0x0002D14F
		[__DynamicallyInvokable]
		public bool IsValueCreated
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_boxed != null && this.m_boxed is Lazy<T>.Boxed;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x0002EF6C File Offset: 0x0002D16C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[__DynamicallyInvokable]
		public T Value
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_boxed != null)
				{
					Lazy<T>.Boxed boxed = this.m_boxed as Lazy<T>.Boxed;
					if (boxed != null)
					{
						return boxed.m_value;
					}
					Lazy<T>.LazyInternalExceptionHolder lazyInternalExceptionHolder = this.m_boxed as Lazy<T>.LazyInternalExceptionHolder;
					lazyInternalExceptionHolder.m_edi.Throw();
				}
				Debugger.NotifyOfCrossThreadDependency();
				return this.LazyInitValue();
			}
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x0002EFBC File Offset: 0x0002D1BC
		private T LazyInitValue()
		{
			Lazy<T>.Boxed boxed = null;
			LazyThreadSafetyMode mode = this.Mode;
			if (mode == LazyThreadSafetyMode.None)
			{
				boxed = this.CreateValue();
				this.m_boxed = boxed;
			}
			else if (mode == LazyThreadSafetyMode.PublicationOnly)
			{
				boxed = this.CreateValue();
				if (boxed == null || Interlocked.CompareExchange(ref this.m_boxed, boxed, null) != null)
				{
					boxed = (Lazy<T>.Boxed)this.m_boxed;
				}
				else
				{
					this.m_valueFactory = Lazy<T>.ALREADY_INVOKED_SENTINEL;
				}
			}
			else
			{
				object obj = Volatile.Read<object>(ref this.m_threadSafeObj);
				bool flag = false;
				try
				{
					if (obj != Lazy<T>.ALREADY_INVOKED_SENTINEL)
					{
						Monitor.Enter(obj, ref flag);
					}
					if (this.m_boxed == null)
					{
						boxed = this.CreateValue();
						this.m_boxed = boxed;
						Volatile.Write<object>(ref this.m_threadSafeObj, Lazy<T>.ALREADY_INVOKED_SENTINEL);
					}
					else
					{
						boxed = (this.m_boxed as Lazy<T>.Boxed);
						if (boxed == null)
						{
							Lazy<T>.LazyInternalExceptionHolder lazyInternalExceptionHolder = this.m_boxed as Lazy<T>.LazyInternalExceptionHolder;
							lazyInternalExceptionHolder.m_edi.Throw();
						}
					}
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(obj);
					}
				}
			}
			return boxed.m_value;
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x0002F0B4 File Offset: 0x0002D2B4
		private Lazy<T>.Boxed CreateValue()
		{
			Lazy<T>.Boxed result = null;
			LazyThreadSafetyMode mode = this.Mode;
			if (this.m_valueFactory != null)
			{
				try
				{
					if (mode != LazyThreadSafetyMode.PublicationOnly && this.m_valueFactory == Lazy<T>.ALREADY_INVOKED_SENTINEL)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Lazy_Value_RecursiveCallsToValue"));
					}
					Func<T> valueFactory = this.m_valueFactory;
					if (mode != LazyThreadSafetyMode.PublicationOnly)
					{
						this.m_valueFactory = Lazy<T>.ALREADY_INVOKED_SENTINEL;
					}
					else if (valueFactory == Lazy<T>.ALREADY_INVOKED_SENTINEL)
					{
						return null;
					}
					return new Lazy<T>.Boxed(valueFactory());
				}
				catch (Exception ex)
				{
					if (mode != LazyThreadSafetyMode.PublicationOnly)
					{
						this.m_boxed = new Lazy<T>.LazyInternalExceptionHolder(ex);
					}
					throw;
				}
			}
			try
			{
				result = new Lazy<T>.Boxed((T)((object)Activator.CreateInstance(typeof(T))));
			}
			catch (MissingMethodException)
			{
				Exception ex2 = new MissingMemberException(Environment.GetResourceString("Lazy_CreateValue_NoParameterlessCtorForT"));
				if (mode != LazyThreadSafetyMode.PublicationOnly)
				{
					this.m_boxed = new Lazy<T>.LazyInternalExceptionHolder(ex2);
				}
				throw ex2;
			}
			return result;
		}

		// Token: 0x0400059A RID: 1434
		private static readonly Func<T> ALREADY_INVOKED_SENTINEL = () => default(T);

		// Token: 0x0400059B RID: 1435
		private object m_boxed;

		// Token: 0x0400059C RID: 1436
		[NonSerialized]
		private Func<T> m_valueFactory;

		// Token: 0x0400059D RID: 1437
		[NonSerialized]
		private object m_threadSafeObj;

		// Token: 0x02000AC0 RID: 2752
		[Serializable]
		private class Boxed
		{
			// Token: 0x060068EE RID: 26862 RVA: 0x0016885F File Offset: 0x00166A5F
			internal Boxed(T value)
			{
				this.m_value = value;
			}

			// Token: 0x040030D4 RID: 12500
			internal T m_value;
		}

		// Token: 0x02000AC1 RID: 2753
		private class LazyInternalExceptionHolder
		{
			// Token: 0x060068EF RID: 26863 RVA: 0x0016886E File Offset: 0x00166A6E
			internal LazyInternalExceptionHolder(Exception ex)
			{
				this.m_edi = ExceptionDispatchInfo.Capture(ex);
			}

			// Token: 0x040030D5 RID: 12501
			internal ExceptionDispatchInfo m_edi;
		}
	}
}
