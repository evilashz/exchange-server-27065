using System;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve
{
	// Token: 0x020008AA RID: 2218
	public class XmlSerializationWriterRecipientSyncState : XmlSerializationWriter
	{
		// Token: 0x06002F93 RID: 12179 RVA: 0x0006C12F File Offset: 0x0006A32F
		public void Write3_RecipientSyncState(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteNullTagLiteral("RecipientSyncState", "");
				return;
			}
			base.TopLevelElement();
			this.Write2_RecipientSyncState("RecipientSyncState", "", (RecipientSyncState)o, true, false);
		}

		// Token: 0x06002F94 RID: 12180 RVA: 0x0006C16C File Offset: 0x0006A36C
		private void Write2_RecipientSyncState(string n, string ns, RecipientSyncState o, bool isNullable, bool needType)
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
				if (!(type == typeof(RecipientSyncState)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("RecipientSyncState", "");
			}
			base.WriteElementString("ProxyAddresses", "", o.ProxyAddresses);
			base.WriteElementString("SignupAddresses", "", o.SignupAddresses);
			base.WriteElementStringRaw("PartnerId", "", XmlConvert.ToString(o.PartnerId));
			base.WriteElementString("UMProxyAddresses", "", o.UMProxyAddresses);
			base.WriteElementString("ArchiveAddress", "", o.ArchiveAddress);
			base.WriteEndElement(o);
		}

		// Token: 0x06002F95 RID: 12181 RVA: 0x0006C247 File Offset: 0x0006A447
		protected override void InitCallbacks()
		{
		}
	}
}
