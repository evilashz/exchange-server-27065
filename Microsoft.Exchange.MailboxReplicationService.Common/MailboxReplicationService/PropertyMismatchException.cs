using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000336 RID: 822
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PropertyMismatchException : MailboxReplicationPermanentException
	{
		// Token: 0x060025C9 RID: 9673 RVA: 0x0005217B File Offset: 0x0005037B
		public PropertyMismatchException(uint propTag, string value1, string value2) : base(MrsStrings.PropertyMismatch(propTag, value1, value2))
		{
			this.propTag = propTag;
			this.value1 = value1;
			this.value2 = value2;
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x000521A0 File Offset: 0x000503A0
		public PropertyMismatchException(uint propTag, string value1, string value2, Exception innerException) : base(MrsStrings.PropertyMismatch(propTag, value1, value2), innerException)
		{
			this.propTag = propTag;
			this.value1 = value1;
			this.value2 = value2;
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x000521C8 File Offset: 0x000503C8
		protected PropertyMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.propTag = (uint)info.GetValue("propTag", typeof(uint));
			this.value1 = (string)info.GetValue("value1", typeof(string));
			this.value2 = (string)info.GetValue("value2", typeof(string));
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x0005223D File Offset: 0x0005043D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("propTag", this.propTag);
			info.AddValue("value1", this.value1);
			info.AddValue("value2", this.value2);
		}

		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x060025CD RID: 9677 RVA: 0x0005227A File Offset: 0x0005047A
		public uint PropTag
		{
			get
			{
				return this.propTag;
			}
		}

		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x060025CE RID: 9678 RVA: 0x00052282 File Offset: 0x00050482
		public string Value1
		{
			get
			{
				return this.value1;
			}
		}

		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x060025CF RID: 9679 RVA: 0x0005228A File Offset: 0x0005048A
		public string Value2
		{
			get
			{
				return this.value2;
			}
		}

		// Token: 0x0400103A RID: 4154
		private readonly uint propTag;

		// Token: 0x0400103B RID: 4155
		private readonly string value1;

		// Token: 0x0400103C RID: 4156
		private readonly string value2;
	}
}
