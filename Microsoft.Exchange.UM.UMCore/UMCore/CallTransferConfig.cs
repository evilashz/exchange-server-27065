using System;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000082 RID: 130
	internal class CallTransferConfig : ActivityConfig
	{
		// Token: 0x060005DE RID: 1502 RVA: 0x0001974A File Offset: 0x0001794A
		internal CallTransferConfig(ActivityManagerConfig manager) : base(manager)
		{
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x00019753 File Offset: 0x00017953
		internal CallTransferType TransferType
		{
			get
			{
				return this.transferType;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0001975B File Offset: 0x0001795B
		internal string PhoneNumber
		{
			get
			{
				return this.phoneNumber;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x00019763 File Offset: 0x00017963
		internal string PhoneNumberType
		{
			get
			{
				return this.phoneNumberType;
			}
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001976B File Offset: 0x0001796B
		internal override ActivityBase CreateActivity(ActivityManager manager)
		{
			return new CallTransfer(manager, this);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00019774 File Offset: 0x00017974
		internal PhoneNumber GetPhoneNumberVariable(ActivityManager manager)
		{
			ExAssert.RetailAssert(this.fsmVariable != null, "Fsm variable is null. We only initialize it when phoneNumberType is Variable");
			return this.fsmVariable.GetValue(manager);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00019798 File Offset: 0x00017998
		protected override void LoadAttributes(XmlNode rootNode)
		{
			base.LoadAttributes(rootNode);
			this.transferType = (CallTransferType)Enum.Parse(typeof(CallTransferType), rootNode.Attributes["type"].Value, true);
			XmlNode xmlNode = rootNode.Attributes["numberType"];
			if (xmlNode == null)
			{
				this.phoneNumberType = "literal";
			}
			else
			{
				this.phoneNumberType = xmlNode.Value;
			}
			if (string.Equals(this.phoneNumberType, "variable", StringComparison.OrdinalIgnoreCase))
			{
				QualifiedName variableName = new QualifiedName(rootNode.Attributes["number"].Value, base.ManagerConfig);
				this.fsmVariable = FsmVariable<Microsoft.Exchange.UM.UMCommon.PhoneNumber>.Create(variableName, base.ManagerConfig);
				return;
			}
			this.phoneNumber = rootNode.Attributes["number"].Value;
		}

		// Token: 0x04000259 RID: 601
		private CallTransferType transferType;

		// Token: 0x0400025A RID: 602
		private string phoneNumber;

		// Token: 0x0400025B RID: 603
		private FsmVariable<PhoneNumber> fsmVariable;

		// Token: 0x0400025C RID: 604
		private string phoneNumberType;
	}
}
