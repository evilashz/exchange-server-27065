using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001CE RID: 462
	[DataContract]
	public class SupportRecipientFilterObject
	{
		// Token: 0x06002521 RID: 9505 RVA: 0x00072040 File Offset: 0x00070240
		public SupportRecipientFilterObject()
		{
			this.OnDeserializing(default(StreamingContext));
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x00072062 File Offset: 0x00070262
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			this.DataObject = new EmailAddressPolicy();
		}

		// Token: 0x17001B6A RID: 7018
		// (get) Token: 0x06002523 RID: 9507 RVA: 0x0007206F File Offset: 0x0007026F
		// (set) Token: 0x06002524 RID: 9508 RVA: 0x00072077 File Offset: 0x00070277
		internal ISupportRecipientFilter DataObject { get; set; }

		// Token: 0x17001B6B RID: 7019
		// (get) Token: 0x06002525 RID: 9509 RVA: 0x00072080 File Offset: 0x00070280
		// (set) Token: 0x06002526 RID: 9510 RVA: 0x0007208D File Offset: 0x0007028D
		[DataMember]
		public ICollection<string> ConditionalCompany
		{
			get
			{
				return this.DataObject.ConditionalCompany;
			}
			set
			{
				this.DataObject.ConditionalCompany = ((value != null && value.Count > 0) ? new MultiValuedProperty<string>(value) : null);
			}
		}

		// Token: 0x17001B6C RID: 7020
		// (get) Token: 0x06002527 RID: 9511 RVA: 0x000720AF File Offset: 0x000702AF
		// (set) Token: 0x06002528 RID: 9512 RVA: 0x000720BC File Offset: 0x000702BC
		[DataMember]
		public ICollection<string> ConditionalCustomAttribute1
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute1;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute1 = ((value != null && value.Count > 0) ? new MultiValuedProperty<string>(value) : null);
			}
		}

		// Token: 0x17001B6D RID: 7021
		// (get) Token: 0x06002529 RID: 9513 RVA: 0x000720DE File Offset: 0x000702DE
		// (set) Token: 0x0600252A RID: 9514 RVA: 0x000720EB File Offset: 0x000702EB
		[DataMember]
		public ICollection<string> ConditionalCustomAttribute2
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute2;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute2 = ((value != null && value.Count > 0) ? new MultiValuedProperty<string>(value) : null);
			}
		}

		// Token: 0x17001B6E RID: 7022
		// (get) Token: 0x0600252B RID: 9515 RVA: 0x0007210D File Offset: 0x0007030D
		// (set) Token: 0x0600252C RID: 9516 RVA: 0x0007211A File Offset: 0x0007031A
		[DataMember]
		public ICollection<string> ConditionalCustomAttribute3
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute3;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute3 = ((value != null && value.Count > 0) ? new MultiValuedProperty<string>(value) : null);
			}
		}

		// Token: 0x17001B6F RID: 7023
		// (get) Token: 0x0600252D RID: 9517 RVA: 0x0007213C File Offset: 0x0007033C
		// (set) Token: 0x0600252E RID: 9518 RVA: 0x00072149 File Offset: 0x00070349
		[DataMember]
		public ICollection<string> ConditionalCustomAttribute4
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute4;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute4 = ((value != null && value.Count > 0) ? new MultiValuedProperty<string>(value) : null);
			}
		}

		// Token: 0x17001B70 RID: 7024
		// (get) Token: 0x0600252F RID: 9519 RVA: 0x0007216B File Offset: 0x0007036B
		// (set) Token: 0x06002530 RID: 9520 RVA: 0x00072178 File Offset: 0x00070378
		[DataMember]
		public ICollection<string> ConditionalCustomAttribute5
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute5;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute5 = ((value != null && value.Count > 0) ? new MultiValuedProperty<string>(value) : null);
			}
		}

		// Token: 0x17001B71 RID: 7025
		// (get) Token: 0x06002531 RID: 9521 RVA: 0x0007219A File Offset: 0x0007039A
		// (set) Token: 0x06002532 RID: 9522 RVA: 0x000721A7 File Offset: 0x000703A7
		[DataMember]
		public ICollection<string> ConditionalCustomAttribute6
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute6;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute6 = ((value != null && value.Count > 0) ? new MultiValuedProperty<string>(value) : null);
			}
		}

		// Token: 0x17001B72 RID: 7026
		// (get) Token: 0x06002533 RID: 9523 RVA: 0x000721C9 File Offset: 0x000703C9
		// (set) Token: 0x06002534 RID: 9524 RVA: 0x000721D6 File Offset: 0x000703D6
		[DataMember]
		public ICollection<string> ConditionalCustomAttribute7
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute7;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute7 = ((value != null && value.Count > 0) ? new MultiValuedProperty<string>(value) : null);
			}
		}

		// Token: 0x17001B73 RID: 7027
		// (get) Token: 0x06002535 RID: 9525 RVA: 0x000721F8 File Offset: 0x000703F8
		// (set) Token: 0x06002536 RID: 9526 RVA: 0x00072205 File Offset: 0x00070405
		[DataMember]
		public ICollection<string> ConditionalCustomAttribute8
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute8;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute8 = ((value != null && value.Count > 0) ? new MultiValuedProperty<string>(value) : null);
			}
		}

		// Token: 0x17001B74 RID: 7028
		// (get) Token: 0x06002537 RID: 9527 RVA: 0x00072227 File Offset: 0x00070427
		// (set) Token: 0x06002538 RID: 9528 RVA: 0x00072234 File Offset: 0x00070434
		[DataMember]
		public ICollection<string> ConditionalCustomAttribute9
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute9;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute9 = ((value != null && value.Count > 0) ? new MultiValuedProperty<string>(value) : null);
			}
		}

		// Token: 0x17001B75 RID: 7029
		// (get) Token: 0x06002539 RID: 9529 RVA: 0x00072256 File Offset: 0x00070456
		// (set) Token: 0x0600253A RID: 9530 RVA: 0x00072263 File Offset: 0x00070463
		[DataMember]
		public ICollection<string> ConditionalCustomAttribute10
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute10;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute10 = ((value != null && value.Count > 0) ? new MultiValuedProperty<string>(value) : null);
			}
		}

		// Token: 0x17001B76 RID: 7030
		// (get) Token: 0x0600253B RID: 9531 RVA: 0x00072285 File Offset: 0x00070485
		// (set) Token: 0x0600253C RID: 9532 RVA: 0x00072292 File Offset: 0x00070492
		[DataMember]
		public ICollection<string> ConditionalCustomAttribute11
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute11;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute11 = ((value != null && value.Count > 0) ? new MultiValuedProperty<string>(value) : null);
			}
		}

		// Token: 0x17001B77 RID: 7031
		// (get) Token: 0x0600253D RID: 9533 RVA: 0x000722B4 File Offset: 0x000704B4
		// (set) Token: 0x0600253E RID: 9534 RVA: 0x000722C1 File Offset: 0x000704C1
		[DataMember]
		public ICollection<string> ConditionalCustomAttribute12
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute12;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute12 = ((value != null && value.Count > 0) ? new MultiValuedProperty<string>(value) : null);
			}
		}

		// Token: 0x17001B78 RID: 7032
		// (get) Token: 0x0600253F RID: 9535 RVA: 0x000722E3 File Offset: 0x000704E3
		// (set) Token: 0x06002540 RID: 9536 RVA: 0x000722F0 File Offset: 0x000704F0
		[DataMember]
		public ICollection<string> ConditionalCustomAttribute13
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute13;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute13 = ((value != null && value.Count > 0) ? new MultiValuedProperty<string>(value) : null);
			}
		}

		// Token: 0x17001B79 RID: 7033
		// (get) Token: 0x06002541 RID: 9537 RVA: 0x00072312 File Offset: 0x00070512
		// (set) Token: 0x06002542 RID: 9538 RVA: 0x0007231F File Offset: 0x0007051F
		[DataMember]
		public ICollection<string> ConditionalCustomAttribute14
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute14;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute14 = ((value != null && value.Count > 0) ? new MultiValuedProperty<string>(value) : null);
			}
		}

		// Token: 0x17001B7A RID: 7034
		// (get) Token: 0x06002543 RID: 9539 RVA: 0x00072341 File Offset: 0x00070541
		// (set) Token: 0x06002544 RID: 9540 RVA: 0x0007234E File Offset: 0x0007054E
		[DataMember]
		public ICollection<string> ConditionalCustomAttribute15
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute15;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute15 = ((value != null && value.Count > 0) ? new MultiValuedProperty<string>(value) : null);
			}
		}

		// Token: 0x17001B7B RID: 7035
		// (get) Token: 0x06002545 RID: 9541 RVA: 0x00072370 File Offset: 0x00070570
		// (set) Token: 0x06002546 RID: 9542 RVA: 0x0007237D File Offset: 0x0007057D
		[DataMember]
		public ICollection<string> ConditionalDepartment
		{
			get
			{
				return this.DataObject.ConditionalDepartment;
			}
			set
			{
				this.DataObject.ConditionalDepartment = ((value != null && value.Count > 0) ? new MultiValuedProperty<string>(value) : null);
			}
		}

		// Token: 0x17001B7C RID: 7036
		// (get) Token: 0x06002547 RID: 9543 RVA: 0x0007239F File Offset: 0x0007059F
		// (set) Token: 0x06002548 RID: 9544 RVA: 0x000723AC File Offset: 0x000705AC
		[DataMember]
		public ICollection<string> ConditionalStateOrProvince
		{
			get
			{
				return this.DataObject.ConditionalStateOrProvince;
			}
			set
			{
				this.DataObject.ConditionalStateOrProvince = ((value != null && value.Count > 0) ? new MultiValuedProperty<string>(value) : null);
			}
		}

		// Token: 0x17001B7D RID: 7037
		// (get) Token: 0x06002549 RID: 9545 RVA: 0x000723D0 File Offset: 0x000705D0
		// (set) Token: 0x0600254A RID: 9546 RVA: 0x000723F6 File Offset: 0x000705F6
		[DataMember]
		public string IncludedRecipients
		{
			get
			{
				return this.DataObject.IncludedRecipients.ToString();
			}
			set
			{
				this.DataObject.IncludedRecipients = new WellKnownRecipientType?((WellKnownRecipientType)Enum.Parse(typeof(WellKnownRecipientType), value));
			}
		}

		// Token: 0x17001B7E RID: 7038
		// (get) Token: 0x0600254B RID: 9547 RVA: 0x0007241D File Offset: 0x0007061D
		// (set) Token: 0x0600254C RID: 9548 RVA: 0x0007243E File Offset: 0x0007063E
		[DataMember]
		public Identity RecipientContainer
		{
			get
			{
				if (this.DataObject.RecipientContainer == null)
				{
					return null;
				}
				return this.DataObject.RecipientContainer.ToIdentity();
			}
			set
			{
				this.DataObject.RecipientContainer = ((value != null) ? ADObjectId.ParseDnOrGuid(value.RawIdentity) : null);
			}
		}

		// Token: 0x17001B7F RID: 7039
		// (get) Token: 0x0600254D RID: 9549 RVA: 0x00072462 File Offset: 0x00070662
		// (set) Token: 0x0600254E RID: 9550 RVA: 0x0007246A File Offset: 0x0007066A
		[DataMember]
		public string LdapRecipientFilter { get; set; }
	}
}
