using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Search.Core
{
	// Token: 0x020000C6 RID: 198
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IndexStatusInvalidPropertyException : IndexStatusException
	{
		// Token: 0x0600060C RID: 1548 RVA: 0x00013065 File Offset: 0x00011265
		public IndexStatusInvalidPropertyException(string property, string value) : base(Strings.IndexStatusInvalidProperty(property, value))
		{
			this.property = property;
			this.value = value;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00013087 File Offset: 0x00011287
		public IndexStatusInvalidPropertyException(string property, string value, Exception innerException) : base(Strings.IndexStatusInvalidProperty(property, value), innerException)
		{
			this.property = property;
			this.value = value;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x000130AC File Offset: 0x000112AC
		protected IndexStatusInvalidPropertyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.property = (string)info.GetValue("property", typeof(string));
			this.value = (string)info.GetValue("value", typeof(string));
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00013101 File Offset: 0x00011301
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("property", this.property);
			info.AddValue("value", this.value);
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x0001312D File Offset: 0x0001132D
		public string Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x00013135 File Offset: 0x00011335
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040002CD RID: 717
		private readonly string property;

		// Token: 0x040002CE RID: 718
		private readonly string value;
	}
}
