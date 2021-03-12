using System;
using System.Runtime.Serialization;
using Microsoft.Office.Compliance.Audit.Schema;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x0200004C RID: 76
	[DataContract]
	public class ThrottlingInfo : IExtensibleDataObject, IVerifiable
	{
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000C263 File Offset: 0x0000A463
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x0000C26B File Offset: 0x0000A46B
		[DataMember(EmitDefaultValue = false)]
		public TimeSpan? Interval { get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000C274 File Offset: 0x0000A474
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x0000C27C File Offset: 0x0000A47C
		ExtensionDataObject IExtensibleDataObject.ExtensionData { get; set; }

		// Token: 0x060002F8 RID: 760 RVA: 0x0000C285 File Offset: 0x0000A485
		public virtual void Initialize()
		{
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000C287 File Offset: 0x0000A487
		public virtual void Validate()
		{
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000C289 File Offset: 0x0000A489
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}
	}
}
