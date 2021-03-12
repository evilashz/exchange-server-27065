using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Globalization
{
	// Token: 0x0200037D RID: 893
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class CultureNotFoundException : ArgumentException, ISerializable
	{
		// Token: 0x06002D83 RID: 11651 RVA: 0x000AD918 File Offset: 0x000ABB18
		[__DynamicallyInvokable]
		public CultureNotFoundException() : base(CultureNotFoundException.DefaultMessage)
		{
		}

		// Token: 0x06002D84 RID: 11652 RVA: 0x000AD925 File Offset: 0x000ABB25
		[__DynamicallyInvokable]
		public CultureNotFoundException(string message) : base(message)
		{
		}

		// Token: 0x06002D85 RID: 11653 RVA: 0x000AD92E File Offset: 0x000ABB2E
		[__DynamicallyInvokable]
		public CultureNotFoundException(string paramName, string message) : base(message, paramName)
		{
		}

		// Token: 0x06002D86 RID: 11654 RVA: 0x000AD938 File Offset: 0x000ABB38
		[__DynamicallyInvokable]
		public CultureNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06002D87 RID: 11655 RVA: 0x000AD942 File Offset: 0x000ABB42
		public CultureNotFoundException(string paramName, int invalidCultureId, string message) : base(message, paramName)
		{
			this.m_invalidCultureId = new int?(invalidCultureId);
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x000AD958 File Offset: 0x000ABB58
		public CultureNotFoundException(string message, int invalidCultureId, Exception innerException) : base(message, innerException)
		{
			this.m_invalidCultureId = new int?(invalidCultureId);
		}

		// Token: 0x06002D89 RID: 11657 RVA: 0x000AD96E File Offset: 0x000ABB6E
		[__DynamicallyInvokable]
		public CultureNotFoundException(string paramName, string invalidCultureName, string message) : base(message, paramName)
		{
			this.m_invalidCultureName = invalidCultureName;
		}

		// Token: 0x06002D8A RID: 11658 RVA: 0x000AD97F File Offset: 0x000ABB7F
		[__DynamicallyInvokable]
		public CultureNotFoundException(string message, string invalidCultureName, Exception innerException) : base(message, innerException)
		{
			this.m_invalidCultureName = invalidCultureName;
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x000AD990 File Offset: 0x000ABB90
		protected CultureNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.m_invalidCultureId = (int?)info.GetValue("InvalidCultureId", typeof(int?));
			this.m_invalidCultureName = (string)info.GetValue("InvalidCultureName", typeof(string));
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x000AD9E8 File Offset: 0x000ABBE8
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			int? num = null;
			num = this.m_invalidCultureId;
			info.AddValue("InvalidCultureId", num, typeof(int?));
			info.AddValue("InvalidCultureName", this.m_invalidCultureName, typeof(string));
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06002D8D RID: 11661 RVA: 0x000ADA50 File Offset: 0x000ABC50
		public virtual int? InvalidCultureId
		{
			get
			{
				return this.m_invalidCultureId;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06002D8E RID: 11662 RVA: 0x000ADA58 File Offset: 0x000ABC58
		[__DynamicallyInvokable]
		public virtual string InvalidCultureName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_invalidCultureName;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06002D8F RID: 11663 RVA: 0x000ADA60 File Offset: 0x000ABC60
		private static string DefaultMessage
		{
			get
			{
				return Environment.GetResourceString("Argument_CultureNotSupported");
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06002D90 RID: 11664 RVA: 0x000ADA6C File Offset: 0x000ABC6C
		private string FormatedInvalidCultureId
		{
			get
			{
				if (this.InvalidCultureId != null)
				{
					return string.Format(CultureInfo.InvariantCulture, "{0} (0x{0:x4})", this.InvalidCultureId.Value);
				}
				return this.InvalidCultureName;
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06002D91 RID: 11665 RVA: 0x000ADAB4 File Offset: 0x000ABCB4
		[__DynamicallyInvokable]
		public override string Message
		{
			[__DynamicallyInvokable]
			get
			{
				string message = base.Message;
				if (this.m_invalidCultureId == null && this.m_invalidCultureName == null)
				{
					return message;
				}
				string resourceString = Environment.GetResourceString("Argument_CultureInvalidIdentifier", new object[]
				{
					this.FormatedInvalidCultureId
				});
				if (message == null)
				{
					return resourceString;
				}
				return message + Environment.NewLine + resourceString;
			}
		}

		// Token: 0x0400129B RID: 4763
		private string m_invalidCultureName;

		// Token: 0x0400129C RID: 4764
		private int? m_invalidCultureId;
	}
}
