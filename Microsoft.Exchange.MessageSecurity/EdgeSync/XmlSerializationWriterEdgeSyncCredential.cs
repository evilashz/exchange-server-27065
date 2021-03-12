using System;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.MessageSecurity.EdgeSync
{
	// Token: 0x02000019 RID: 25
	internal class XmlSerializationWriterEdgeSyncCredential : XmlSerializationWriter
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00004F12 File Offset: 0x00003112
		public void Write3_EdgeSyncCredential(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteNullTagLiteral("EdgeSyncCredential", "");
				return;
			}
			base.TopLevelElement();
			this.Write2_EdgeSyncCredential("EdgeSyncCredential", "", (EdgeSyncCredential)o, true, false);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004F4C File Offset: 0x0000314C
		private void Write2_EdgeSyncCredential(string n, string ns, EdgeSyncCredential o, bool isNullable, bool needType)
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
				if (!(type == typeof(EdgeSyncCredential)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("EdgeSyncCredential", "");
			}
			base.WriteElementString("EdgeServerFQDN", "", o.EdgeServerFQDN);
			base.WriteElementString("ESRAUsername", "", o.ESRAUsername);
			base.WriteElementStringRaw("EncryptedESRAPassword", "", XmlSerializationWriter.FromByteArrayBase64(o.EncryptedESRAPassword));
			base.WriteElementStringRaw("EdgeEncryptedESRAPassword", "", XmlSerializationWriter.FromByteArrayBase64(o.EdgeEncryptedESRAPassword));
			base.WriteElementStringRaw("EffectiveDate", "", XmlConvert.ToString(o.EffectiveDate));
			base.WriteElementStringRaw("Duration", "", XmlConvert.ToString(o.Duration));
			base.WriteElementStringRaw("IsBootStrapAccount", "", XmlConvert.ToString(o.IsBootStrapAccount));
			base.WriteEndElement(o);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00005067 File Offset: 0x00003267
		protected override void InitCallbacks()
		{
		}
	}
}
