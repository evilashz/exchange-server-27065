using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000D5 RID: 213
	[DataContract]
	public class EnumObject
	{
		// Token: 0x06001D6C RID: 7532 RVA: 0x0005A433 File Offset: 0x00058633
		public EnumObject(Enum e)
		{
			this.e = e;
		}

		// Token: 0x17001951 RID: 6481
		// (get) Token: 0x06001D6D RID: 7533 RVA: 0x0005A442 File Offset: 0x00058642
		// (set) Token: 0x06001D6E RID: 7534 RVA: 0x0005A45A File Offset: 0x0005865A
		[DataMember]
		public string Description
		{
			get
			{
				return LocalizedDescriptionAttribute.FromEnum(this.e.GetType(), this.e);
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17001952 RID: 6482
		// (get) Token: 0x06001D6F RID: 7535 RVA: 0x0005A461 File Offset: 0x00058661
		// (set) Token: 0x06001D70 RID: 7536 RVA: 0x0005A46E File Offset: 0x0005866E
		[DataMember]
		public string Value
		{
			get
			{
				return this.e.ToString();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x0005A478 File Offset: 0x00058678
		public override bool Equals(object obj)
		{
			EnumObject enumObject = obj as EnumObject;
			return enumObject != null && enumObject.Value == this.Value;
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x0005A4A2 File Offset: 0x000586A2
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x04001BDF RID: 7135
		private Enum e;
	}
}
