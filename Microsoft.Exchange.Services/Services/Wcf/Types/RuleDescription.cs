using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B0B RID: 2827
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RuleDescription
	{
		// Token: 0x0600502F RID: 20527 RVA: 0x001093F5 File Offset: 0x001075F5
		public RuleDescription()
		{
		}

		// Token: 0x06005030 RID: 20528 RVA: 0x00109400 File Offset: 0x00107600
		public RuleDescription(RuleDescription ruleDescription)
		{
			this.ActionDescriptions = ruleDescription.ActionDescriptions.ToArray();
			this.ActivationDescription = ruleDescription.ActivationDescription;
			this.ConditionDescriptions = ruleDescription.ConditionDescriptions.ToArray();
			this.ExceptionDescriptions = ruleDescription.ExceptionDescriptions.ToArray();
			this.ExpiryDescription = ruleDescription.ExpiryDescription;
			this.RuleDescriptionActivation = ruleDescription.RuleDescriptionActivation;
			this.RuleDescriptionExceptIf = ruleDescription.RuleDescriptionExceptIf;
			this.RuleDescriptionExpiry = ruleDescription.RuleDescriptionExpiry;
			this.RuleDescriptionIf = ruleDescription.RuleDescriptionIf;
			this.RuleDescriptionTakeActions = ruleDescription.RuleDescriptionTakeActions;
		}

		// Token: 0x17001325 RID: 4901
		// (get) Token: 0x06005031 RID: 20529 RVA: 0x0010949A File Offset: 0x0010769A
		// (set) Token: 0x06005032 RID: 20530 RVA: 0x001094A2 File Offset: 0x001076A2
		[DataMember]
		public string[] ActionDescriptions { get; private set; }

		// Token: 0x17001326 RID: 4902
		// (get) Token: 0x06005033 RID: 20531 RVA: 0x001094AB File Offset: 0x001076AB
		// (set) Token: 0x06005034 RID: 20532 RVA: 0x001094B3 File Offset: 0x001076B3
		[DataMember]
		public string ActivationDescription { get; private set; }

		// Token: 0x17001327 RID: 4903
		// (get) Token: 0x06005035 RID: 20533 RVA: 0x001094BC File Offset: 0x001076BC
		// (set) Token: 0x06005036 RID: 20534 RVA: 0x001094C4 File Offset: 0x001076C4
		[DataMember]
		public string[] ConditionDescriptions { get; private set; }

		// Token: 0x17001328 RID: 4904
		// (get) Token: 0x06005037 RID: 20535 RVA: 0x001094CD File Offset: 0x001076CD
		// (set) Token: 0x06005038 RID: 20536 RVA: 0x001094D5 File Offset: 0x001076D5
		[DataMember]
		public string[] ExceptionDescriptions { get; private set; }

		// Token: 0x17001329 RID: 4905
		// (get) Token: 0x06005039 RID: 20537 RVA: 0x001094DE File Offset: 0x001076DE
		// (set) Token: 0x0600503A RID: 20538 RVA: 0x001094E6 File Offset: 0x001076E6
		[DataMember]
		public string ExpiryDescription { get; private set; }

		// Token: 0x1700132A RID: 4906
		// (get) Token: 0x0600503B RID: 20539 RVA: 0x001094EF File Offset: 0x001076EF
		// (set) Token: 0x0600503C RID: 20540 RVA: 0x001094F7 File Offset: 0x001076F7
		[DataMember]
		public string RuleDescriptionActivation { get; private set; }

		// Token: 0x1700132B RID: 4907
		// (get) Token: 0x0600503D RID: 20541 RVA: 0x00109500 File Offset: 0x00107700
		// (set) Token: 0x0600503E RID: 20542 RVA: 0x00109508 File Offset: 0x00107708
		[DataMember]
		public string RuleDescriptionExceptIf { get; private set; }

		// Token: 0x1700132C RID: 4908
		// (get) Token: 0x0600503F RID: 20543 RVA: 0x00109511 File Offset: 0x00107711
		// (set) Token: 0x06005040 RID: 20544 RVA: 0x00109519 File Offset: 0x00107719
		[DataMember]
		public string RuleDescriptionExpiry { get; private set; }

		// Token: 0x1700132D RID: 4909
		// (get) Token: 0x06005041 RID: 20545 RVA: 0x00109522 File Offset: 0x00107722
		// (set) Token: 0x06005042 RID: 20546 RVA: 0x0010952A File Offset: 0x0010772A
		[DataMember]
		public string RuleDescriptionIf { get; private set; }

		// Token: 0x1700132E RID: 4910
		// (get) Token: 0x06005043 RID: 20547 RVA: 0x00109533 File Offset: 0x00107733
		// (set) Token: 0x06005044 RID: 20548 RVA: 0x0010953B File Offset: 0x0010773B
		[DataMember]
		public string RuleDescriptionTakeActions { get; private set; }

		// Token: 0x06005045 RID: 20549 RVA: 0x00109544 File Offset: 0x00107744
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("{ActionDescriptions = ");
			stringBuilder.Append(this.CreateStringList(this.ActionDescriptions));
			stringBuilder.Append(", ActivationDescription = \"");
			stringBuilder.Append(this.ActivationDescription);
			stringBuilder.Append("\", ConditionDescriptions = ");
			stringBuilder.Append(this.CreateStringList(this.ConditionDescriptions));
			stringBuilder.Append(", ExceptionDescriptions = \"");
			stringBuilder.Append(this.CreateStringList(this.ExceptionDescriptions));
			stringBuilder.Append(", ExpiryDescription = \"");
			stringBuilder.Append(this.ExpiryDescription);
			stringBuilder.Append("\", RuleDescriptionActivation = \"");
			stringBuilder.Append(this.RuleDescriptionActivation);
			stringBuilder.Append("\", RuleDescriptionExceptIf = \"");
			stringBuilder.Append(this.RuleDescriptionExceptIf);
			stringBuilder.Append("\", RuleDescriptionExpiry = \"");
			stringBuilder.Append(this.RuleDescriptionExpiry);
			stringBuilder.Append("\", RuleDescriptionIf = \"");
			stringBuilder.Append(this.RuleDescriptionIf);
			stringBuilder.Append("\", RuleDescriptionTakeActions = \"");
			stringBuilder.Append(this.RuleDescriptionTakeActions);
			stringBuilder.Append("\"}");
			return stringBuilder.ToString();
		}

		// Token: 0x06005046 RID: 20550 RVA: 0x0010968C File Offset: 0x0010788C
		private string CreateStringList(IEnumerable<string> values)
		{
			if (values == null || !values.Any<string>())
			{
				return "{}";
			}
			return "{" + string.Join(",", from e in values
			select "\"" + (e ?? string.Empty) + "\"") + "}";
		}
	}
}
