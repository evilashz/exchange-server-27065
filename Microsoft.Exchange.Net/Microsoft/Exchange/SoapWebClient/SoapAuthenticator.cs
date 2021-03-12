using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Web.Services.Protocols;
using System.Xml;
using Microsoft.Exchange.Net.WSSecurity;
using Microsoft.Exchange.Net.WSTrust;

namespace Microsoft.Exchange.SoapWebClient
{
	// Token: 0x020006D7 RID: 1751
	internal abstract class SoapAuthenticator
	{
		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x060020B6 RID: 8374 RVA: 0x00040F3F File Offset: 0x0003F13F
		protected virtual XmlNamespaceDefinition[] PredefinedNamespaces
		{
			get
			{
				return SoapAuthenticator.NoPredefinedNamespaces;
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x060020B7 RID: 8375 RVA: 0x00040F46 File Offset: 0x0003F146
		public SoapHeaderCollection AdditionalSoapHeaders
		{
			get
			{
				if (this.additionalSoapHeaders == null)
				{
					this.additionalSoapHeaders = new SoapHeaderCollection();
				}
				return this.additionalSoapHeaders;
			}
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x00040F64 File Offset: 0x0003F164
		protected virtual void AddMessageSoapHeaders(SoapClientMessage message)
		{
			if (this.AdditionalSoapHeaders != null && this.AdditionalSoapHeaders.Count > 0)
			{
				foreach (object obj in this.AdditionalSoapHeaders)
				{
					SoapHeader header = (SoapHeader)obj;
					message.Headers.Add(header);
				}
			}
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x00040FDC File Offset: 0x0003F1DC
		public XmlReader GetReaderForMessage(XmlReader xmlReader, SoapClientMessage message)
		{
			return new SoapHttpClientXmlReader(xmlReader);
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x00040FE4 File Offset: 0x0003F1E4
		public XmlWriter GetWriterForMessage(XmlNamespaceDefinition[] otherPredefinedNamespaces, XmlWriter xmlWriter, SoapClientMessage message)
		{
			this.AddMessageSoapHeaders(message);
			XmlNamespaceDefinition[] namespaceDefinitions;
			if (otherPredefinedNamespaces.Length > 0 && this.PredefinedNamespaces.Length > 0)
			{
				List<XmlNamespaceDefinition> list = new List<XmlNamespaceDefinition>(otherPredefinedNamespaces.Length + this.PredefinedNamespaces.Length);
				list.AddRange(otherPredefinedNamespaces);
				list.AddRange(this.PredefinedNamespaces);
				namespaceDefinitions = list.ToArray();
			}
			else if (otherPredefinedNamespaces.Length > 0)
			{
				namespaceDefinitions = otherPredefinedNamespaces;
			}
			else
			{
				namespaceDefinitions = this.PredefinedNamespaces;
			}
			return new SoapHttpClientXmlWriter(xmlWriter, namespaceDefinitions);
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x0004104F File Offset: 0x0003F24F
		public static SoapAuthenticator CreateNone()
		{
			return new SoapAuthenticator.NoSoapAuthenticator();
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x00041056 File Offset: 0x0003F256
		public static SoapAuthenticator CreateAnonymous()
		{
			return new SoapAuthenticator.AnonymousSoapAuthenticator();
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x0004105D File Offset: 0x0003F25D
		public static SoapAuthenticator Create(X509Certificate2 certificate)
		{
			return SoapAuthenticator.Create(WSSecurityHeader.Create(certificate));
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x0004106A File Offset: 0x0003F26A
		public static SoapAuthenticator Create(RequestedToken token)
		{
			return SoapAuthenticator.Create(WSSecurityHeader.Create(token));
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x00041077 File Offset: 0x0003F277
		public static SoapAuthenticator Create(WSSecurityHeader header)
		{
			return new SoapAuthenticator.WSSecurityAuthenticator(header);
		}

		// Token: 0x04001F6D RID: 8045
		private SoapHeaderCollection additionalSoapHeaders;

		// Token: 0x04001F6E RID: 8046
		private static XmlNamespaceDefinition[] NoPredefinedNamespaces = new XmlNamespaceDefinition[0];

		// Token: 0x020006D8 RID: 1752
		private sealed class NoSoapAuthenticator : SoapAuthenticator
		{
		}

		// Token: 0x020006D9 RID: 1753
		private class AnonymousSoapAuthenticator : SoapAuthenticator
		{
			// Token: 0x17000888 RID: 2184
			// (get) Token: 0x060020C3 RID: 8387 RVA: 0x0004109C File Offset: 0x0003F29C
			protected override XmlNamespaceDefinition[] PredefinedNamespaces
			{
				get
				{
					return SoapAuthenticator.AnonymousSoapAuthenticator.wsAddressingPredefinedNamespaces;
				}
			}

			// Token: 0x060020C4 RID: 8388 RVA: 0x000410A4 File Offset: 0x0003F2A4
			protected override void AddMessageSoapHeaders(SoapClientMessage message)
			{
				base.AddMessageSoapHeaders(message);
				message.Headers.Add(new WSAddressingActionHeader(message.Action));
				message.Headers.Add(new WSAddressingToHeader(message.Url));
				message.Headers.Add(WSAddressingReplyToHeader.Anonymous);
			}

			// Token: 0x04001F6F RID: 8047
			private static XmlNamespaceDefinition[] wsAddressingPredefinedNamespaces = new XmlNamespaceDefinition[]
			{
				WSAddressing.Namespace
			};
		}

		// Token: 0x020006DA RID: 1754
		private sealed class WSSecurityAuthenticator : SoapAuthenticator.AnonymousSoapAuthenticator
		{
			// Token: 0x17000889 RID: 2185
			// (get) Token: 0x060020C7 RID: 8391 RVA: 0x00041122 File Offset: 0x0003F322
			protected override XmlNamespaceDefinition[] PredefinedNamespaces
			{
				get
				{
					return SoapAuthenticator.WSSecurityAuthenticator.wsSecurityPredefinedNamespaces;
				}
			}

			// Token: 0x060020C8 RID: 8392 RVA: 0x00041129 File Offset: 0x0003F329
			public WSSecurityAuthenticator(WSSecurityHeader wsSecurityHeader)
			{
				this.wsSecurityHeader = wsSecurityHeader;
			}

			// Token: 0x060020C9 RID: 8393 RVA: 0x00041138 File Offset: 0x0003F338
			protected override void AddMessageSoapHeaders(SoapClientMessage message)
			{
				base.AddMessageSoapHeaders(message);
				message.Headers.Add(this.wsSecurityHeader);
			}

			// Token: 0x04001F70 RID: 8048
			private WSSecurityHeader wsSecurityHeader;

			// Token: 0x04001F71 RID: 8049
			private static XmlNamespaceDefinition[] wsSecurityPredefinedNamespaces = new XmlNamespaceDefinition[]
			{
				WSAddressing.Namespace,
				WSSecurityExtensions.Namespace,
				XmlDigitalSignature.Namespace,
				XmlEncryption.Namespace
			};
		}
	}
}
