using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001C7 RID: 455
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class ClientSessionInfo
	{
		// Token: 0x06001869 RID: 6249 RVA: 0x00076EA8 File Offset: 0x000750A8
		private ClientSessionInfo()
		{
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x0600186A RID: 6250 RVA: 0x00076EB0 File Offset: 0x000750B0
		// (set) Token: 0x0600186B RID: 6251 RVA: 0x00076EB8 File Offset: 0x000750B8
		public LogonType EffectiveLogonType { get; private set; }

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x0600186C RID: 6252 RVA: 0x00076EC1 File Offset: 0x000750C1
		// (set) Token: 0x0600186D RID: 6253 RVA: 0x00076EC9 File Offset: 0x000750C9
		public string LogonUserSid { get; private set; }

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x0600186E RID: 6254 RVA: 0x00076ED2 File Offset: 0x000750D2
		// (set) Token: 0x0600186F RID: 6255 RVA: 0x00076EDA File Offset: 0x000750DA
		public string LogonUserDisplayName { get; private set; }

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06001870 RID: 6256 RVA: 0x00076EE3 File Offset: 0x000750E3
		// (set) Token: 0x06001871 RID: 6257 RVA: 0x00076EEB File Offset: 0x000750EB
		public string ClientInfoString { get; private set; }

		// Token: 0x06001872 RID: 6258 RVA: 0x00076EF4 File Offset: 0x000750F4
		public static byte[] WrapInfoForRemoteServer(MailboxSession session)
		{
			Util.ThrowOnNullArgument(session, "session");
			if (!session.IsRemote)
			{
				throw new InvalidOperationException("Only remote session supports ClientSessionInfo.");
			}
			if (!session.MailboxOwner.MailboxInfo.Configuration.IsMailboxAuditEnabled)
			{
				return null;
			}
			IdentityPair identityPair = IdentityHelper.GetIdentityPair(session);
			ClientSessionInfo clientSessionInfo = new ClientSessionInfo
			{
				EffectiveLogonType = COWAudit.ResolveEffectiveLogonType(session, null, null),
				ClientInfoString = session.ClientInfoString,
				LogonUserSid = identityPair.LogonUserSid,
				LogonUserDisplayName = identityPair.LogonUserDisplayName
			};
			return clientSessionInfo.ToBytes();
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x00076F8C File Offset: 0x0007518C
		public static ClientSessionInfo FromBytes(byte[] blob)
		{
			if (blob == null)
			{
				return null;
			}
			ClientSessionInfo result;
			try
			{
				BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(ClientSessionInfo.serializationBinder);
				using (MemoryStream memoryStream = new MemoryStream(blob))
				{
					ClientSessionInfo clientSessionInfo = (ClientSessionInfo)binaryFormatter.Deserialize(memoryStream);
					result = clientSessionInfo;
				}
			}
			catch (FormatException)
			{
				throw new ClientSessionInfoParseException();
			}
			return result;
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x00076FF0 File Offset: 0x000751F0
		public byte[] ToBytes()
		{
			BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(2048))
			{
				binaryFormatter.Serialize(memoryStream, this);
				byte[] array = memoryStream.ToArray();
				result = array;
			}
			return result;
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x00077040 File Offset: 0x00075240
		internal static byte[] InternalTestGetFakeSerializedBlob()
		{
			ClientSessionInfo clientSessionInfo = new ClientSessionInfo
			{
				EffectiveLogonType = LogonType.BestAccess,
				ClientInfoString = "FakeTestSession",
				LogonUserSid = "S-Fake-Test-SID",
				LogonUserDisplayName = "Fake User Display Name"
			};
			return clientSessionInfo.ToBytes();
		}

		// Token: 0x04000CCF RID: 3279
		private const int MaxBlobSize = 2048;

		// Token: 0x04000CD0 RID: 3280
		private static SerializationBinder serializationBinder = new ClientSessionInfo.ClientSessionInfoDeserializationBinder();

		// Token: 0x020001C8 RID: 456
		private sealed class ClientSessionInfoDeserializationBinder : SerializationBinder
		{
			// Token: 0x06001877 RID: 6263 RVA: 0x00077090 File Offset: 0x00075290
			public override Type BindToType(string assemblyName, string typeName)
			{
				Type type = null;
				if (typeName == typeof(ClientSessionInfo).FullName)
				{
					type = typeof(ClientSessionInfo);
				}
				else if (typeName == typeof(LogonType).FullName)
				{
					type = typeof(LogonType);
				}
				if (type == null)
				{
					throw new ClientSessionInfoTypeParseException(typeName, assemblyName);
				}
				return type;
			}
		}
	}
}
