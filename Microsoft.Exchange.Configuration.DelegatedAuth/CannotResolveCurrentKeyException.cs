using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.DelegatedAuthentication.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.DelegatedAuthentication
{
	// Token: 0x02000011 RID: 17
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotResolveCurrentKeyException : LocalizedException
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00004650 File Offset: 0x00002850
		public CannotResolveCurrentKeyException(bool currentKey) : base(Strings.CannotResolveCurrentKeyException(currentKey))
		{
			this.currentKey = currentKey;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004665 File Offset: 0x00002865
		public CannotResolveCurrentKeyException(bool currentKey, Exception innerException) : base(Strings.CannotResolveCurrentKeyException(currentKey), innerException)
		{
			this.currentKey = currentKey;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000467B File Offset: 0x0000287B
		protected CannotResolveCurrentKeyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.currentKey = (bool)info.GetValue("currentKey", typeof(bool));
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000046A5 File Offset: 0x000028A5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("currentKey", this.currentKey);
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000046C0 File Offset: 0x000028C0
		public bool CurrentKey
		{
			get
			{
				return this.currentKey;
			}
		}

		// Token: 0x04000058 RID: 88
		private readonly bool currentKey;
	}
}
