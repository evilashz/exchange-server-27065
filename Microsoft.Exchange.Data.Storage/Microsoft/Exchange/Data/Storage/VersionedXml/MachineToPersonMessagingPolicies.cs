using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Storage.VersionedXml
{
	// Token: 0x02000ED4 RID: 3796
	[Serializable]
	public class MachineToPersonMessagingPolicies
	{
		// Token: 0x06008301 RID: 33537 RVA: 0x0023A4DE File Offset: 0x002386DE
		public MachineToPersonMessagingPolicies()
		{
		}

		// Token: 0x06008302 RID: 33538 RVA: 0x0023A4E6 File Offset: 0x002386E6
		public MachineToPersonMessagingPolicies(IEnumerable<PossibleRecipient> possibleRecipients)
		{
			if (possibleRecipients != null)
			{
				this.PossibleRecipients = new List<PossibleRecipient>(possibleRecipients);
			}
		}

		// Token: 0x170022C0 RID: 8896
		// (get) Token: 0x06008303 RID: 33539 RVA: 0x0023A4FD File Offset: 0x002386FD
		// (set) Token: 0x06008304 RID: 33540 RVA: 0x0023A50A File Offset: 0x0023870A
		[XmlElement("PossibleRecipient")]
		public List<PossibleRecipient> PossibleRecipients
		{
			get
			{
				return AccessorTemplates.ListPropertyGetter<PossibleRecipient>(ref this.possibleRecipients);
			}
			set
			{
				AccessorTemplates.ListPropertySetter<PossibleRecipient>(ref this.possibleRecipients, value);
			}
		}

		// Token: 0x170022C1 RID: 8897
		// (get) Token: 0x06008305 RID: 33541 RVA: 0x0023A518 File Offset: 0x00238718
		[XmlIgnore]
		public IList<PossibleRecipient> EffectivePossibleRecipients
		{
			get
			{
				return PossibleRecipient.GetCandidates(this.PossibleRecipients, true);
			}
		}

		// Token: 0x170022C2 RID: 8898
		// (get) Token: 0x06008306 RID: 33542 RVA: 0x0023A526 File Offset: 0x00238726
		[XmlIgnore]
		public IList<PossibleRecipient> NoneffectivePossibleRecipients
		{
			get
			{
				return PossibleRecipient.GetCandidates(this.PossibleRecipients, false);
			}
		}

		// Token: 0x040057D3 RID: 22483
		private List<PossibleRecipient> possibleRecipients;
	}
}
