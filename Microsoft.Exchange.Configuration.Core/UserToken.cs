using System;
using System.IO;
using System.IO.Compression;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Configuration.Core.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.Configuration.Core;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x0200000E RID: 14
	public class UserToken
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002E5C File Offset: 0x0000105C
		internal UserToken(AuthenticationType authenticationType, DelegatedPrincipal delegatedPrincipal, string windowsLiveId, string userName, SecurityIdentifier userSid, PartitionId partitionId, OrganizationId organization, string managedOrganization, bool appPasswordUsed, CommonAccessToken commonAccessToken)
		{
			ExTraceGlobals.UserTokenTracer.TraceDebug(0L, "Version:{0}; AuthenticationType:{1}; DelegatedPrincipal:{2} WindowsLiveId:{3}; UserName:{4}; UserSid:{5}; PartitionId:{6}; Organization:{7}; ManagedOrg:{8};AppPasswordUsed:{9}; CAT:{10}", new object[]
			{
				0,
				authenticationType,
				delegatedPrincipal,
				windowsLiveId,
				userName,
				userSid,
				partitionId,
				organization,
				managedOrganization,
				appPasswordUsed,
				commonAccessToken
			});
			this.Version = 0;
			this.AuthenticationType = authenticationType;
			this.DelegatedPrincipal = delegatedPrincipal;
			this.WindowsLiveId = windowsLiveId;
			this.UserName = userName;
			this.UserSid = userSid;
			this.PartitionId = partitionId;
			this.Organization = organization;
			this.ManagedOrganization = managedOrganization;
			this.AppPasswordUsed = appPasswordUsed;
			this.CommonAccessToken = commonAccessToken;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002F21 File Offset: 0x00001121
		private UserToken(Stream stream)
		{
			this.Deserialize(stream);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002F30 File Offset: 0x00001130
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002F38 File Offset: 0x00001138
		internal ushort Version { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002F41 File Offset: 0x00001141
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002F49 File Offset: 0x00001149
		internal AuthenticationType AuthenticationType { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002F52 File Offset: 0x00001152
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002F5A File Offset: 0x0000115A
		internal DelegatedPrincipal DelegatedPrincipal { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002F63 File Offset: 0x00001163
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002F6B File Offset: 0x0000116B
		internal string WindowsLiveId { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002F74 File Offset: 0x00001174
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002F7C File Offset: 0x0000117C
		internal string UserName { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002F85 File Offset: 0x00001185
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002F8D File Offset: 0x0000118D
		internal SecurityIdentifier UserSid { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002F96 File Offset: 0x00001196
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002F9E File Offset: 0x0000119E
		internal PartitionId PartitionId { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002FA7 File Offset: 0x000011A7
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002FAF File Offset: 0x000011AF
		internal OrganizationId Organization { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002FB8 File Offset: 0x000011B8
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002FC0 File Offset: 0x000011C0
		internal string ManagedOrganization { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002FC9 File Offset: 0x000011C9
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002FD1 File Offset: 0x000011D1
		internal CommonAccessToken CommonAccessToken { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002FDA File Offset: 0x000011DA
		internal bool HasCommonAccessToken
		{
			get
			{
				return this.CommonAccessToken != null;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002FE8 File Offset: 0x000011E8
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002FF0 File Offset: 0x000011F0
		internal bool AppPasswordUsed { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002FF9 File Offset: 0x000011F9
		internal ADRawEntry Recipient
		{
			get
			{
				return UserTokenStaticHelper.GetADRawEntry(this);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003004 File Offset: 0x00001204
		public static UserToken Deserialize(string token)
		{
			ExTraceGlobals.UserTokenTracer.TraceDebug<string>(0L, "token={0}", token);
			if (token == null)
			{
				throw new ArgumentNullException("token");
			}
			UserToken result;
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(token)))
				{
					result = new UserToken(memoryStream);
				}
			}
			catch (Exception ex)
			{
				ExTraceGlobals.UserTokenTracer.TraceError<Exception>(0L, "Exception from Deserialize: {0}", ex);
				throw new UserTokenException(ex.Message, ex);
			}
			return result;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003090 File Offset: 0x00001290
		public string Serialize()
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					this.Serialize(binaryWriter);
				}
				string text = Convert.ToBase64String(memoryStream.ToArray());
				ExTraceGlobals.UserTokenTracer.TraceDebug<string>(0L, "SerializedString={0}", text);
				result = text;
			}
			return result;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003108 File Offset: 0x00001308
		public override string ToString()
		{
			return string.Format("Version:{0}; AuthenticationType:{1}; DelegatedPrincipal:{2} WindowsLiveId:{3}; UserName:{4}; UserSid:{5}; PartitionId:{6}; Organization:{7}; ManagedOrg:{8};AppPasswordUsed:{9}; CAT:{10}", new object[]
			{
				this.Version,
				this.AuthenticationType,
				this.DelegatedPrincipal,
				this.WindowsLiveId,
				this.UserName,
				this.UserSid,
				this.PartitionId,
				this.Organization,
				this.ManagedOrganization,
				this.AppPasswordUsed,
				this.CommonAccessToken
			});
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000319C File Offset: 0x0000139C
		private void Serialize(BinaryWriter writer)
		{
			writer.Write('V');
			writer.Write(this.Version);
			writer.Write('A');
			writer.Write(this.AuthenticationType.ToString());
			writer.Write('D');
			this.WriteNullableValue(writer, (this.DelegatedPrincipal != null) ? this.DelegatedPrincipal.Identity.Name : null);
			writer.Write('L');
			this.WriteNullableValue(writer, this.WindowsLiveId);
			writer.Write('N');
			this.WriteNullableValue(writer, this.UserName);
			writer.Write('U');
			this.WriteNullableValue(writer, (this.UserSid != null) ? this.UserSid.ToString() : null);
			writer.Write('P');
			this.WriteNullableValue(writer, (this.PartitionId != null) ? this.PartitionId.ToString() : null);
			writer.Write('O');
			string value = null;
			if (this.Organization != null)
			{
				value = Convert.ToBase64String(this.Organization.GetBytes(Encoding.UTF8));
			}
			this.WriteNullableValue(writer, value);
			writer.Write('M');
			this.WriteNullableValue(writer, this.ManagedOrganization);
			writer.Write('W');
			writer.Write(this.AppPasswordUsed);
			if (this.CommonAccessToken != null)
			{
				byte[] bytes = Encoding.UTF8.GetBytes(this.CommonAccessToken.Serialize());
				using (GZipStream gzipStream = new GZipStream(writer.BaseStream, CompressionMode.Compress, true))
				{
					gzipStream.Write(bytes, 0, bytes.Length);
				}
			}
			writer.Flush();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003340 File Offset: 0x00001540
		private void WriteNullableValue(BinaryWriter writer, string value)
		{
			if (value == null)
			{
				writer.Write("0");
				return;
			}
			writer.Write(value.Replace("0", "\\0"));
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003368 File Offset: 0x00001568
		private void Deserialize(Stream stream)
		{
			using (BinaryReader binaryReader = new BinaryReader(stream))
			{
				this.ReadAndValidateFieldType(binaryReader, 'V', Strings.MissingVersion);
				this.Version = this.BinaryRead<ushort>(new Func<ushort>(binaryReader.ReadUInt16), Strings.MissingVersion);
				this.ReadAndValidateFieldType(binaryReader, 'A', Strings.MissingAuthenticationType);
				string value = this.BinaryRead<string>(new Func<string>(binaryReader.ReadString), Strings.MissingAuthenticationType);
				AuthenticationType authenticationType;
				if (!Enum.TryParse<AuthenticationType>(value, out authenticationType))
				{
					ExTraceGlobals.UserTokenTracer.TraceError<AuthenticationType>(0L, "Invalid authentication type {0}", authenticationType);
					throw new UserTokenException(Strings.InvalidDelegatedPrincipal(value));
				}
				this.AuthenticationType = authenticationType;
				this.ReadAndValidateFieldType(binaryReader, 'D', Strings.MissingDelegatedPrincipal);
				string text = this.ReadNullableString(binaryReader, Strings.MissingDelegatedPrincipal);
				DelegatedPrincipal delegatedPrincipal = null;
				if (text != null && !DelegatedPrincipal.TryParseDelegatedString(text, out delegatedPrincipal))
				{
					ExTraceGlobals.UserTokenTracer.TraceError<string>(0L, "Invalid delegated principal {0}", text);
					throw new UserTokenException(Strings.InvalidDelegatedPrincipal(text));
				}
				this.DelegatedPrincipal = delegatedPrincipal;
				this.ReadAndValidateFieldType(binaryReader, 'L', Strings.MissingWindowsLiveId);
				this.WindowsLiveId = this.ReadNullableString(binaryReader, Strings.MissingWindowsLiveId);
				this.ReadAndValidateFieldType(binaryReader, 'N', Strings.MissingUserName);
				this.UserName = this.ReadNullableString(binaryReader, Strings.MissingUserName);
				this.ReadAndValidateFieldType(binaryReader, 'U', Strings.MissingUserSid);
				string text2 = this.ReadNullableString(binaryReader, Strings.MissingUserSid);
				if (text2 != null)
				{
					try
					{
						this.UserSid = new SecurityIdentifier(text2);
					}
					catch (ArgumentException innerException)
					{
						ExTraceGlobals.UserTokenTracer.TraceError<string>(0L, "Invalid user sid {0}", text2);
						throw new UserTokenException(Strings.InvalidUserSid(text2), innerException);
					}
				}
				this.ReadAndValidateFieldType(binaryReader, 'P', Strings.MissingPartitionId);
				string text3 = this.ReadNullableString(binaryReader, Strings.MissingPartitionId);
				PartitionId partitionId = null;
				if (text3 != null && !PartitionId.TryParse(text3, out partitionId))
				{
					ExTraceGlobals.UserTokenTracer.TraceError<string>(0L, "Invalid partition id {0}", text3);
					throw new UserTokenException(Strings.InvalidPartitionId(text3));
				}
				this.PartitionId = partitionId;
				this.ReadAndValidateFieldType(binaryReader, 'O', Strings.MissingOrganization);
				string text4 = this.ReadNullableString(binaryReader, Strings.MissingOrganization);
				if (text4 != null)
				{
					byte[] bytes;
					try
					{
						bytes = Convert.FromBase64String(text4);
					}
					catch (FormatException innerException2)
					{
						ExTraceGlobals.UserTokenTracer.TraceError<string>(0L, "Invalid organization id {0}", text4);
						throw new UserTokenException(Strings.InvalidOrganization(text4), innerException2);
					}
					OrganizationId organization;
					if (!OrganizationId.TryCreateFromBytes(bytes, Encoding.UTF8, out organization))
					{
						ExTraceGlobals.UserTokenTracer.TraceError<string>(0L, "Invalid organization id {0}", text4);
						throw new UserTokenException(Strings.InvalidOrganization(text4));
					}
					this.Organization = organization;
				}
				this.ReadAndValidateFieldType(binaryReader, 'M', Strings.MissingManagedOrganization);
				this.ManagedOrganization = this.ReadNullableString(binaryReader, Strings.MissingManagedOrganization);
				this.ReadAndValidateFieldType(binaryReader, 'W', Strings.MissingAppPasswordUsed);
				this.AppPasswordUsed = this.BinaryRead<bool>(new Func<bool>(binaryReader.ReadBoolean), Strings.MissingAppPasswordUsed);
				int num = (int)(binaryReader.BaseStream.Length - binaryReader.BaseStream.Position);
				if (num > 0)
				{
					byte[] array = binaryReader.ReadBytes(num);
					array = this.Decompress(array);
					this.CommonAccessToken = CommonAccessToken.Deserialize(Encoding.UTF8.GetString(array));
				}
				else
				{
					this.CommonAccessToken = null;
				}
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000036D4 File Offset: 0x000018D4
		private void ReadAndValidateFieldType(BinaryReader reader, char fieldType, LocalizedString errorMessage)
		{
			char c = this.BinaryRead<char>(new Func<char>(reader.ReadChar), errorMessage);
			if (c != fieldType)
			{
				ExTraceGlobals.UserTokenTracer.TraceError<char>(0L, "Invalid field char {0}", c);
				throw new UserTokenException(errorMessage);
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003718 File Offset: 0x00001918
		private string ReadNullableString(BinaryReader reader, LocalizedString errorMessage)
		{
			string result;
			try
			{
				string text = reader.ReadString();
				if ("0".Equals(text))
				{
					result = null;
				}
				else
				{
					result = text.Replace("\\0", "0");
				}
			}
			catch (EndOfStreamException ex)
			{
				ExTraceGlobals.UserTokenTracer.TraceError<EndOfStreamException>(0L, "Unexpected end of stream. Exception: {0}", ex);
				throw new UserTokenException(errorMessage, ex);
			}
			return result;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003784 File Offset: 0x00001984
		private T BinaryRead<T>(Func<T> readMethod, LocalizedString errorMessage)
		{
			T result;
			try
			{
				result = readMethod();
			}
			catch (EndOfStreamException ex)
			{
				ExTraceGlobals.UserTokenTracer.TraceError<EndOfStreamException>(0L, "Unexpected end of stream. Exception: {0}", ex);
				throw new UserTokenException(errorMessage, ex);
			}
			return result;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000037CC File Offset: 0x000019CC
		private byte[] Decompress(byte[] compressedBytes)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (MemoryStream memoryStream2 = new MemoryStream(compressedBytes))
				{
					using (GZipStream gzipStream = new GZipStream(memoryStream2, CompressionMode.Decompress))
					{
						gzipStream.CopyTo(memoryStream);
					}
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x0400002E RID: 46
		public const string KeyToStoreUserToken = "X-EX-UserToken";

		// Token: 0x0400002F RID: 47
		private const string NullString = "0";

		// Token: 0x04000030 RID: 48
		private const string TranslatedZeroString = "\\0";

		// Token: 0x04000031 RID: 49
		private const string StringFmt = "Version:{0}; AuthenticationType:{1}; DelegatedPrincipal:{2} WindowsLiveId:{3}; UserName:{4}; UserSid:{5}; PartitionId:{6}; Organization:{7}; ManagedOrg:{8};AppPasswordUsed:{9}; CAT:{10}";
	}
}
