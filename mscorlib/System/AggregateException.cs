using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	// Token: 0x02000039 RID: 57
	[DebuggerDisplay("Count = {InnerExceptionCount}")]
	[__DynamicallyInvokable]
	[Serializable]
	public class AggregateException : Exception
	{
		// Token: 0x060001EE RID: 494 RVA: 0x00004DEE File Offset: 0x00002FEE
		[__DynamicallyInvokable]
		public AggregateException() : base(Environment.GetResourceString("AggregateException_ctor_DefaultMessage"))
		{
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(new Exception[0]);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00004E11 File Offset: 0x00003011
		[__DynamicallyInvokable]
		public AggregateException(string message) : base(message)
		{
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(new Exception[0]);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00004E2B File Offset: 0x0000302B
		[__DynamicallyInvokable]
		public AggregateException(string message, Exception innerException) : base(message, innerException)
		{
			if (innerException == null)
			{
				throw new ArgumentNullException("innerException");
			}
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(new Exception[]
			{
				innerException
			});
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00004E58 File Offset: 0x00003058
		[__DynamicallyInvokable]
		public AggregateException(IEnumerable<Exception> innerExceptions) : this(Environment.GetResourceString("AggregateException_ctor_DefaultMessage"), innerExceptions)
		{
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00004E6B File Offset: 0x0000306B
		[__DynamicallyInvokable]
		public AggregateException(params Exception[] innerExceptions) : this(Environment.GetResourceString("AggregateException_ctor_DefaultMessage"), innerExceptions)
		{
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00004E7E File Offset: 0x0000307E
		[__DynamicallyInvokable]
		public AggregateException(string message, IEnumerable<Exception> innerExceptions) : this(message, (innerExceptions as IList<Exception>) ?? ((innerExceptions == null) ? null : new List<Exception>(innerExceptions)))
		{
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00004E9D File Offset: 0x0000309D
		[__DynamicallyInvokable]
		public AggregateException(string message, params Exception[] innerExceptions) : this(message, innerExceptions)
		{
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00004EA8 File Offset: 0x000030A8
		private AggregateException(string message, IList<Exception> innerExceptions) : base(message, (innerExceptions != null && innerExceptions.Count > 0) ? innerExceptions[0] : null)
		{
			if (innerExceptions == null)
			{
				throw new ArgumentNullException("innerExceptions");
			}
			Exception[] array = new Exception[innerExceptions.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = innerExceptions[i];
				if (array[i] == null)
				{
					throw new ArgumentException(Environment.GetResourceString("AggregateException_ctor_InnerExceptionNull"));
				}
			}
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(array);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00004F25 File Offset: 0x00003125
		internal AggregateException(IEnumerable<ExceptionDispatchInfo> innerExceptionInfos) : this(Environment.GetResourceString("AggregateException_ctor_DefaultMessage"), innerExceptionInfos)
		{
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00004F38 File Offset: 0x00003138
		internal AggregateException(string message, IEnumerable<ExceptionDispatchInfo> innerExceptionInfos) : this(message, (innerExceptionInfos as IList<ExceptionDispatchInfo>) ?? ((innerExceptionInfos == null) ? null : new List<ExceptionDispatchInfo>(innerExceptionInfos)))
		{
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00004F58 File Offset: 0x00003158
		private AggregateException(string message, IList<ExceptionDispatchInfo> innerExceptionInfos) : base(message, (innerExceptionInfos != null && innerExceptionInfos.Count > 0 && innerExceptionInfos[0] != null) ? innerExceptionInfos[0].SourceException : null)
		{
			if (innerExceptionInfos == null)
			{
				throw new ArgumentNullException("innerExceptionInfos");
			}
			Exception[] array = new Exception[innerExceptionInfos.Count];
			for (int i = 0; i < array.Length; i++)
			{
				ExceptionDispatchInfo exceptionDispatchInfo = innerExceptionInfos[i];
				if (exceptionDispatchInfo != null)
				{
					array[i] = exceptionDispatchInfo.SourceException;
				}
				if (array[i] == null)
				{
					throw new ArgumentException(Environment.GetResourceString("AggregateException_ctor_InnerExceptionNull"));
				}
			}
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(array);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00004FF0 File Offset: 0x000031F0
		[SecurityCritical]
		protected AggregateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			Exception[] array = info.GetValue("InnerExceptions", typeof(Exception[])) as Exception[];
			if (array == null)
			{
				throw new SerializationException(Environment.GetResourceString("AggregateException_DeserializationFailure"));
			}
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(array);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00005050 File Offset: 0x00003250
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			Exception[] array = new Exception[this.m_innerExceptions.Count];
			this.m_innerExceptions.CopyTo(array, 0);
			info.AddValue("InnerExceptions", array, typeof(Exception[]));
		}

		// Token: 0x060001FB RID: 507 RVA: 0x000050A8 File Offset: 0x000032A8
		[__DynamicallyInvokable]
		public override Exception GetBaseException()
		{
			Exception ex = this;
			AggregateException ex2 = this;
			while (ex2 != null && ex2.InnerExceptions.Count == 1)
			{
				ex = ex.InnerException;
				ex2 = (ex as AggregateException);
			}
			return ex;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060001FC RID: 508 RVA: 0x000050DB File Offset: 0x000032DB
		[__DynamicallyInvokable]
		public ReadOnlyCollection<Exception> InnerExceptions
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_innerExceptions;
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000050E4 File Offset: 0x000032E4
		[__DynamicallyInvokable]
		public void Handle(Func<Exception, bool> predicate)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			List<Exception> list = null;
			for (int i = 0; i < this.m_innerExceptions.Count; i++)
			{
				if (!predicate(this.m_innerExceptions[i]))
				{
					if (list == null)
					{
						list = new List<Exception>();
					}
					list.Add(this.m_innerExceptions[i]);
				}
			}
			if (list != null)
			{
				throw new AggregateException(this.Message, list);
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00005158 File Offset: 0x00003358
		[__DynamicallyInvokable]
		public AggregateException Flatten()
		{
			List<Exception> list = new List<Exception>();
			List<AggregateException> list2 = new List<AggregateException>();
			list2.Add(this);
			int num = 0;
			while (list2.Count > num)
			{
				IList<Exception> innerExceptions = list2[num++].InnerExceptions;
				for (int i = 0; i < innerExceptions.Count; i++)
				{
					Exception ex = innerExceptions[i];
					if (ex != null)
					{
						AggregateException ex2 = ex as AggregateException;
						if (ex2 != null)
						{
							list2.Add(ex2);
						}
						else
						{
							list.Add(ex);
						}
					}
				}
			}
			return new AggregateException(this.Message, list);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000051E4 File Offset: 0x000033E4
		[__DynamicallyInvokable]
		public override string ToString()
		{
			string text = base.ToString();
			for (int i = 0; i < this.m_innerExceptions.Count; i++)
			{
				text = string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("AggregateException_ToString"), new object[]
				{
					text,
					Environment.NewLine,
					i,
					this.m_innerExceptions[i].ToString(),
					"<---",
					Environment.NewLine
				});
			}
			return text;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00005263 File Offset: 0x00003463
		private int InnerExceptionCount
		{
			get
			{
				return this.InnerExceptions.Count;
			}
		}

		// Token: 0x040001C7 RID: 455
		private ReadOnlyCollection<Exception> m_innerExceptions;
	}
}
