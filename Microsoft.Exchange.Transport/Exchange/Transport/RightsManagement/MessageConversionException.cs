using System;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.RightsManagement
{
	// Token: 0x020003E6 RID: 998
	[Serializable]
	internal class MessageConversionException : LocalizedException
	{
		// Token: 0x06002D84 RID: 11652 RVA: 0x000B6087 File Offset: 0x000B4287
		public MessageConversionException(LocalizedString message, bool isTransient) : base(message)
		{
			this.isTransient = isTransient;
		}

		// Token: 0x06002D85 RID: 11653 RVA: 0x000B6097 File Offset: 0x000B4297
		public MessageConversionException(LocalizedString message, Exception innerException, bool isTransient) : base(message, innerException)
		{
			this.isTransient = isTransient;
		}

		// Token: 0x06002D86 RID: 11654 RVA: 0x000B60A8 File Offset: 0x000B42A8
		protected MessageConversionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.isTransient = info.GetBoolean("IsTransient");
		}

		// Token: 0x06002D87 RID: 11655 RVA: 0x000B60C3 File Offset: 0x000B42C3
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("IsTransient", this.isTransient);
		}

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x06002D88 RID: 11656 RVA: 0x000B60EC File Offset: 0x000B42EC
		public bool IsTransient
		{
			get
			{
				return this.isTransient;
			}
		}

		// Token: 0x04001696 RID: 5782
		private const string SerializationIsTransientAttributeName = "IsTransient";

		// Token: 0x04001697 RID: 5783
		private bool isTransient;
	}
}
