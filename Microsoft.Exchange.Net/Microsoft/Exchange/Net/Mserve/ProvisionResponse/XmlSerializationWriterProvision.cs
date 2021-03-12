using System;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.ProvisionResponse
{
	// Token: 0x020008A2 RID: 2210
	internal class XmlSerializationWriterProvision : XmlSerializationWriter
	{
		// Token: 0x06002F55 RID: 12117 RVA: 0x0006A983 File Offset: 0x00068B83
		public void Write6_Provision(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("Provision", "DeltaSyncV2:");
				return;
			}
			base.TopLevelElement();
			this.Write5_Provision("Provision", "DeltaSyncV2:", (Provision)o, false, false);
		}

		// Token: 0x06002F56 RID: 12118 RVA: 0x0006A9C0 File Offset: 0x00068BC0
		private void Write5_Provision(string n, string ns, Provision o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(Provision)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType(null, "DeltaSyncV2:");
			}
			base.WriteElementStringRaw("Status", "DeltaSyncV2:", XmlConvert.ToString(o.Status));
			this.Write2_Fault("Fault", "DeltaSyncV2:", o.Fault, false, false);
			this.Write4_ProvisionResponses("Responses", "DeltaSyncV2:", o.Responses, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x06002F57 RID: 12119 RVA: 0x0006AA70 File Offset: 0x00068C70
		private void Write4_ProvisionResponses(string n, string ns, ProvisionResponses o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(ProvisionResponses)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType(null, "DeltaSyncV2:");
			}
			AccountType[] add = o.Add;
			if (add != null)
			{
				base.WriteStartElement("Add", "DeltaSyncV2:", null, false);
				for (int i = 0; i < add.Length; i++)
				{
					this.Write3_AccountType("Account", "DeltaSyncV2:", add[i], false, false);
				}
				base.WriteEndElement();
			}
			AccountType[] delete = o.Delete;
			if (delete != null)
			{
				base.WriteStartElement("Delete", "DeltaSyncV2:", null, false);
				for (int j = 0; j < delete.Length; j++)
				{
					this.Write3_AccountType("Account", "DeltaSyncV2:", delete[j], false, false);
				}
				base.WriteEndElement();
			}
			AccountType[] read = o.Read;
			if (read != null)
			{
				base.WriteStartElement("Read", "DeltaSyncV2:", null, false);
				for (int k = 0; k < read.Length; k++)
				{
					this.Write3_AccountType("Account", "DeltaSyncV2:", read[k], false, false);
				}
				base.WriteEndElement();
			}
			base.WriteEndElement(o);
		}

		// Token: 0x06002F58 RID: 12120 RVA: 0x0006ABB4 File Offset: 0x00068DB4
		private void Write3_AccountType(string n, string ns, AccountType o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(AccountType)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("AccountType", "DeltaSyncV2:");
			}
			base.WriteElementString("Name", "DeltaSyncV2:", o.Name);
			base.WriteElementStringRaw("Status", "DeltaSyncV2:", XmlConvert.ToString(o.Status));
			this.Write2_Fault("Fault", "DeltaSyncV2:", o.Fault, false, false);
			base.WriteElementString("PartnerID", "DeltaSyncV2:", o.PartnerID);
			base.WriteEndElement(o);
		}

		// Token: 0x06002F59 RID: 12121 RVA: 0x0006AC7C File Offset: 0x00068E7C
		private void Write2_Fault(string n, string ns, Fault o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(Fault)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType(null, "DeltaSyncV2:");
			}
			base.WriteElementString("Faultcode", "DeltaSyncV2:", o.Faultcode);
			base.WriteElementString("Faultstring", "DeltaSyncV2:", o.Faultstring);
			base.WriteElementString("Detail", "DeltaSyncV2:", o.Detail);
			base.WriteEndElement(o);
		}

		// Token: 0x06002F5A RID: 12122 RVA: 0x0006AD22 File Offset: 0x00068F22
		protected override void InitCallbacks()
		{
		}
	}
}
