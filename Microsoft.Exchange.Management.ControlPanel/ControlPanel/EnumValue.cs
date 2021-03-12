using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200041C RID: 1052
	[DataContract]
	public class EnumValue : BaseRow
	{
		// Token: 0x06003545 RID: 13637 RVA: 0x000A58DE File Offset: 0x000A3ADE
		public EnumValue(Enum value) : base(null, null)
		{
			this.cachedValue = value;
			this.Value = value.ToString();
		}

		// Token: 0x06003546 RID: 13638 RVA: 0x000A58FB File Offset: 0x000A3AFB
		public EnumValue(Enum value, bool useLocalizedValue) : this(value)
		{
			this.useLocalizedValue = useLocalizedValue;
		}

		// Token: 0x06003547 RID: 13639 RVA: 0x000A590B File Offset: 0x000A3B0B
		public EnumValue(string displayText, string value) : base(null, null)
		{
			this.displayText = displayText;
			this.Value = value;
		}

		// Token: 0x170020EA RID: 8426
		// (get) Token: 0x06003548 RID: 13640 RVA: 0x000A5923 File Offset: 0x000A3B23
		// (set) Token: 0x06003549 RID: 13641 RVA: 0x000A594A File Offset: 0x000A3B4A
		[DataMember]
		public string DisplayText
		{
			get
			{
				if (this.displayText != null)
				{
					return this.displayText;
				}
				return LocalizedDescriptionAttribute.FromEnum(this.cachedValue.GetType(), this.cachedValue);
			}
			protected set
			{
				this.displayText = value;
			}
		}

		// Token: 0x170020EB RID: 8427
		// (get) Token: 0x0600354A RID: 13642 RVA: 0x000A5953 File Offset: 0x000A3B53
		// (set) Token: 0x0600354B RID: 13643 RVA: 0x000A597A File Offset: 0x000A3B7A
		[DataMember]
		public string Value
		{
			get
			{
				if (this.useLocalizedValue)
				{
					return LocalizedDescriptionAttribute.FromEnum(this.cachedValue.GetType(), this.cachedValue);
				}
				return this.value;
			}
			protected set
			{
				this.value = value;
			}
		}

		// Token: 0x0400257D RID: 9597
		private Enum cachedValue;

		// Token: 0x0400257E RID: 9598
		private bool useLocalizedValue;

		// Token: 0x0400257F RID: 9599
		private string displayText;

		// Token: 0x04002580 RID: 9600
		private string value;
	}
}
