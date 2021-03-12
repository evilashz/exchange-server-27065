using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000412 RID: 1042
	[DataContract]
	public abstract class JournalRuleObjectProperties : SetObjectProperties
	{
		// Token: 0x170020D1 RID: 8401
		// (get) Token: 0x06003506 RID: 13574 RVA: 0x000A52EC File Offset: 0x000A34EC
		// (set) Token: 0x06003507 RID: 13575 RVA: 0x000A52FE File Offset: 0x000A34FE
		[DataMember]
		public string Name
		{
			get
			{
				return (string)base["Name"];
			}
			set
			{
				base["Name"] = value;
			}
		}

		// Token: 0x170020D2 RID: 8402
		// (get) Token: 0x06003508 RID: 13576 RVA: 0x000A530C File Offset: 0x000A350C
		public override string RbacScope
		{
			get
			{
				return "@W:Organization";
			}
		}

		// Token: 0x170020D3 RID: 8403
		// (get) Token: 0x06003509 RID: 13577 RVA: 0x000A5313 File Offset: 0x000A3513
		// (set) Token: 0x0600350A RID: 13578 RVA: 0x000A5325 File Offset: 0x000A3525
		[DataMember]
		public string JournalEmailAddress
		{
			get
			{
				return (string)base["JournalEmailAddress"];
			}
			set
			{
				base["JournalEmailAddress"] = value;
			}
		}

		// Token: 0x170020D4 RID: 8404
		// (get) Token: 0x0600350B RID: 13579 RVA: 0x000A5334 File Offset: 0x000A3534
		// (set) Token: 0x0600350C RID: 13580 RVA: 0x000A5388 File Offset: 0x000A3588
		[DataMember]
		public PeopleIdentity[] Recipient
		{
			get
			{
				SmtpAddress? smtpAddress = (SmtpAddress?)base["Recipient"];
				List<ADIdParameter> list = new List<ADIdParameter>();
				if (smtpAddress != null)
				{
					list.Add(RecipientIdParameter.Parse(smtpAddress.Value.ToString()));
				}
				return PeopleIdentity.FromIdParameters(list);
			}
			set
			{
				SmtpAddress? smtpAddress = null;
				if (value != null && value.Length > 0)
				{
					smtpAddress = new SmtpAddress?(SmtpAddress.Parse(value[0].SMTPAddress));
				}
				base["Recipient"] = smtpAddress;
			}
		}

		// Token: 0x170020D5 RID: 8405
		// (get) Token: 0x0600350D RID: 13581 RVA: 0x000A53CD File Offset: 0x000A35CD
		// (set) Token: 0x0600350E RID: 13582 RVA: 0x000A53DF File Offset: 0x000A35DF
		[DataMember]
		public string Scope
		{
			get
			{
				return (string)base["Scope"];
			}
			set
			{
				base["Scope"] = value;
			}
		}

		// Token: 0x170020D6 RID: 8406
		// (get) Token: 0x0600350F RID: 13583 RVA: 0x000A53F0 File Offset: 0x000A35F0
		// (set) Token: 0x06003510 RID: 13584 RVA: 0x000A542A File Offset: 0x000A362A
		[DataMember]
		public bool? Global
		{
			get
			{
				string text = (string)base["Scope"];
				return new bool?(!string.IsNullOrEmpty(text) && text.Equals("Global", StringComparison.InvariantCultureIgnoreCase));
			}
			set
			{
				if (value != null && value.Value)
				{
					base["Scope"] = "Global";
				}
			}
		}

		// Token: 0x170020D7 RID: 8407
		// (get) Token: 0x06003511 RID: 13585 RVA: 0x000A5450 File Offset: 0x000A3650
		// (set) Token: 0x06003512 RID: 13586 RVA: 0x000A548A File Offset: 0x000A368A
		[DataMember]
		public bool? Internal
		{
			get
			{
				string text = (string)base["Scope"];
				return new bool?(!string.IsNullOrEmpty(text) && text.Equals("Internal", StringComparison.InvariantCultureIgnoreCase));
			}
			set
			{
				if (value != null && value.Value)
				{
					base["Scope"] = "Internal";
				}
			}
		}

		// Token: 0x170020D8 RID: 8408
		// (get) Token: 0x06003513 RID: 13587 RVA: 0x000A54B0 File Offset: 0x000A36B0
		// (set) Token: 0x06003514 RID: 13588 RVA: 0x000A54EA File Offset: 0x000A36EA
		[DataMember]
		public bool? External
		{
			get
			{
				string text = (string)base["Scope"];
				return new bool?(!string.IsNullOrEmpty(text) && text.Equals("External", StringComparison.InvariantCultureIgnoreCase));
			}
			set
			{
				if (value != null && value.Value)
				{
					base["Scope"] = "External";
				}
			}
		}
	}
}
