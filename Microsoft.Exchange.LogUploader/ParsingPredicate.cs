using System;
using System.Runtime.Serialization;
using Microsoft.Office.Compliance.Audit.Schema;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000049 RID: 73
	[DataContract]
	public class ParsingPredicate : IExtensibleDataObject, IVerifiable
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000C063 File Offset: 0x0000A263
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x0000C06B File Offset: 0x0000A26B
		[DataMember(IsRequired = true)]
		public Parsing ParsingOutcome { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000C074 File Offset: 0x0000A274
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x0000C07C File Offset: 0x0000A27C
		ExtensionDataObject IExtensibleDataObject.ExtensionData { get; set; }

		// Token: 0x060002D6 RID: 726 RVA: 0x0000C085 File Offset: 0x0000A285
		public virtual void Initialize()
		{
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000C087 File Offset: 0x0000A287
		public virtual void Validate()
		{
			if (!Enum.IsDefined(typeof(Parsing), this.ParsingOutcome))
			{
				throw new InvalidEnumValueException("ParsingPredicate", "ParsingOutcome", this.ParsingOutcome);
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000C0C0 File Offset: 0x0000A2C0
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}
	}
}
