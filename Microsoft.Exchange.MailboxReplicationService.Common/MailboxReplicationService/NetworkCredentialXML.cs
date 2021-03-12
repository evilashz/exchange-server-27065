using System;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Security.Cryptography;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200017B RID: 379
	public sealed class NetworkCredentialXML : XMLSerializableBase
	{
		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000E5B RID: 3675 RVA: 0x00020B33 File Offset: 0x0001ED33
		// (set) Token: 0x06000E5C RID: 3676 RVA: 0x00020B3B File Offset: 0x0001ED3B
		[XmlElement]
		public string Username { get; set; }

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x00020B44 File Offset: 0x0001ED44
		// (set) Token: 0x06000E5E RID: 3678 RVA: 0x00020B4C File Offset: 0x0001ED4C
		[XmlElement]
		public byte[] EncryptedPassword { get; set; }

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x00020B55 File Offset: 0x0001ED55
		// (set) Token: 0x06000E60 RID: 3680 RVA: 0x00020B5D File Offset: 0x0001ED5D
		[XmlElement]
		public string Domain { get; set; }

		// Token: 0x06000E61 RID: 3681 RVA: 0x00020B68 File Offset: 0x0001ED68
		internal static NetworkCredentialXML Get(NetworkCredential nc)
		{
			if (nc == null)
			{
				return null;
			}
			NetworkCredentialXML networkCredentialXML = new NetworkCredentialXML();
			networkCredentialXML.Username = nc.UserName;
			networkCredentialXML.Domain = nc.Domain;
			if (string.IsNullOrEmpty(nc.Password))
			{
				networkCredentialXML.EncryptedPassword = null;
			}
			else
			{
				networkCredentialXML.EncryptedPassword = CryptoTools.Encrypt(Encoding.Unicode.GetBytes(nc.Password), NetworkCredentialXML.PwdEncryptionKey);
			}
			return networkCredentialXML;
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x00020BD0 File Offset: 0x0001EDD0
		internal static NetworkCredential Get(NetworkCredentialXML value)
		{
			if (value == null)
			{
				return null;
			}
			string password;
			if (value.EncryptedPassword != null)
			{
				byte[] bytes = CryptoTools.Decrypt(value.EncryptedPassword, NetworkCredentialXML.PwdEncryptionKey);
				password = Encoding.Unicode.GetString(bytes);
			}
			else
			{
				password = null;
			}
			return new NetworkCredential(value.Username, password, value.Domain);
		}

		// Token: 0x04000815 RID: 2069
		private static readonly byte[] PwdEncryptionKey = new Guid("2bb81a8d-f5d1-48c9-99c1-bb2dda57f66d").ToByteArray();
	}
}
