using System;
using System.Runtime.Serialization;
using Microsoft.Office.Compliance.Audit.Schema;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000048 RID: 72
	[DataContract]
	public class ParsingRule : IExtensibleDataObject, IVerifiable
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000BF9C File Offset: 0x0000A19C
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x0000BFA4 File Offset: 0x0000A1A4
		[DataMember(EmitDefaultValue = false, IsRequired = true)]
		public ParsingPredicate Predicate { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000BFAD File Offset: 0x0000A1AD
		// (set) Token: 0x060002CB RID: 715 RVA: 0x0000BFB5 File Offset: 0x0000A1B5
		[DataMember(IsRequired = true)]
		public Actions ActionToPerform { get; set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000BFBE File Offset: 0x0000A1BE
		// (set) Token: 0x060002CD RID: 717 RVA: 0x0000BFC6 File Offset: 0x0000A1C6
		ExtensionDataObject IExtensibleDataObject.ExtensionData { get; set; }

		// Token: 0x060002CE RID: 718 RVA: 0x0000BFCF File Offset: 0x0000A1CF
		public virtual void Initialize()
		{
			if (this.Predicate != null)
			{
				this.Predicate.Initialize();
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000BFE4 File Offset: 0x0000A1E4
		public virtual void Validate()
		{
			if (this.Predicate == null)
			{
				throw new ExpectedValueException("ParsingRule", "Predicate");
			}
			if (this.Predicate != null)
			{
				this.Predicate.Validate();
			}
			if (!Enum.IsDefined(typeof(Actions), this.ActionToPerform))
			{
				throw new InvalidEnumValueException("ParsingRule", "ActionToPerform", this.ActionToPerform);
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000C053 File Offset: 0x0000A253
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}
	}
}
