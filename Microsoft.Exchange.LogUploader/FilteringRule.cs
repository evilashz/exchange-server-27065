using System;
using System.Runtime.Serialization;
using Microsoft.Office.Compliance.Audit.Schema;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x0200004A RID: 74
	[DataContract]
	public class FilteringRule : IExtensibleDataObject, IVerifiable
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000C0D0 File Offset: 0x0000A2D0
		// (set) Token: 0x060002DB RID: 731 RVA: 0x0000C0D8 File Offset: 0x0000A2D8
		[DataMember(EmitDefaultValue = false, IsRequired = true)]
		public FilteringPredicate Predicate { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000C0E1 File Offset: 0x0000A2E1
		// (set) Token: 0x060002DD RID: 733 RVA: 0x0000C0E9 File Offset: 0x0000A2E9
		[DataMember(IsRequired = true)]
		public Actions ActionToPerform { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000C0F2 File Offset: 0x0000A2F2
		// (set) Token: 0x060002DF RID: 735 RVA: 0x0000C0FA File Offset: 0x0000A2FA
		[DataMember(EmitDefaultValue = false)]
		public ThrottlingInfo Throttle { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000C103 File Offset: 0x0000A303
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x0000C10B File Offset: 0x0000A30B
		ExtensionDataObject IExtensibleDataObject.ExtensionData { get; set; }

		// Token: 0x060002E2 RID: 738 RVA: 0x0000C114 File Offset: 0x0000A314
		public virtual void Initialize()
		{
			if (this.Predicate != null)
			{
				this.Predicate.Initialize();
			}
			if (this.Throttle != null)
			{
				this.Throttle.Initialize();
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000C13C File Offset: 0x0000A33C
		public virtual void Validate()
		{
			if (this.Predicate == null)
			{
				throw new ExpectedValueException("FilteringRule", "Predicate");
			}
			if (this.Predicate != null)
			{
				this.Predicate.Validate();
			}
			if (!Enum.IsDefined(typeof(Actions), this.ActionToPerform))
			{
				throw new InvalidEnumValueException("FilteringRule", "ActionToPerform", this.ActionToPerform);
			}
			if (this.Throttle != null)
			{
				this.Throttle.Validate();
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000C1BE File Offset: 0x0000A3BE
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}
	}
}
