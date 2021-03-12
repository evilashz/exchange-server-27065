using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Mapi.Common
{
	// Token: 0x02000046 RID: 70
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MapiCalculatedPropertySettingException : MapiConvertingException
	{
		// Token: 0x06000286 RID: 646 RVA: 0x0000E209 File Offset: 0x0000C409
		public MapiCalculatedPropertySettingException(string name, string value, string details) : base(Strings.MapiCalculatedPropertySettingExceptionError(name, value, details))
		{
			this.name = name;
			this.value = value;
			this.details = details;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000E22E File Offset: 0x0000C42E
		public MapiCalculatedPropertySettingException(string name, string value, string details, Exception innerException) : base(Strings.MapiCalculatedPropertySettingExceptionError(name, value, details), innerException)
		{
			this.name = name;
			this.value = value;
			this.details = details;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000E258 File Offset: 0x0000C458
		protected MapiCalculatedPropertySettingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
			this.value = (string)info.GetValue("value", typeof(string));
			this.details = (string)info.GetValue("details", typeof(string));
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000E2CD File Offset: 0x0000C4CD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
			info.AddValue("value", this.value);
			info.AddValue("details", this.details);
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000E30A File Offset: 0x0000C50A
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000E312 File Offset: 0x0000C512
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000E31A File Offset: 0x0000C51A
		public string Details
		{
			get
			{
				return this.details;
			}
		}

		// Token: 0x0400019B RID: 411
		private readonly string name;

		// Token: 0x0400019C RID: 412
		private readonly string value;

		// Token: 0x0400019D RID: 413
		private readonly string details;
	}
}
