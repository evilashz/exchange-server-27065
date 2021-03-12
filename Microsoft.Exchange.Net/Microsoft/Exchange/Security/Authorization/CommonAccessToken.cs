using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x02000648 RID: 1608
	public sealed class CommonAccessToken
	{
		// Token: 0x06001D0F RID: 7439 RVA: 0x00034AA8 File Offset: 0x00032CA8
		static CommonAccessToken()
		{
			Type typeFromHandle = typeof(ExtensionDataKey);
			FieldInfo[] fields = typeFromHandle.GetFields(BindingFlags.Static | BindingFlags.Public);
			if (fields != null)
			{
				CommonAccessToken.ValidExtensionDataKeys = new List<string>(from f in fields
				select f.GetValue(null).ToString());
			}
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x00034AF9 File Offset: 0x00032CF9
		public CommonAccessToken(AccessTokenType accessTokenType) : this(accessTokenType, false)
		{
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x00034B03 File Offset: 0x00032D03
		public CommonAccessToken(AccessTokenType accessTokenType, bool shouldCompress)
		{
			this.Version = 1;
			this.TokenType = accessTokenType.ToString();
			this.IsCompressed = shouldCompress;
			this.ExtensionData = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x00034B3A File Offset: 0x00032D3A
		public CommonAccessToken(WindowsIdentity windowsIdentity) : this(windowsIdentity, false)
		{
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x00034B44 File Offset: 0x00032D44
		public CommonAccessToken(WindowsIdentity windowsIdentity, bool shouldCompress)
		{
			if (windowsIdentity == null)
			{
				throw new ArgumentNullException("windowsIdentity");
			}
			this.Version = 1;
			this.TokenType = AccessTokenType.Windows.ToString();
			this.IsCompressed = shouldCompress;
			this.ExtensionData = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			using (ClientSecurityContext clientSecurityContext = new ClientSecurityContext(windowsIdentity))
			{
				try
				{
					this.WindowsAccessToken = new WindowsAccessToken(windowsIdentity.GetSafeName(false), windowsIdentity.AuthenticationType, clientSecurityContext, this);
				}
				catch (SystemException innerException)
				{
					throw new CommonAccessTokenException(0, AuthorizationStrings.LogonNameIsMissing, innerException);
				}
			}
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x00034BF0 File Offset: 0x00032DF0
		internal CommonAccessToken(string logonName, string authenticationType, ClientSecurityContext clientSecurityContext, bool shouldCompress)
		{
			this.Version = 1;
			this.TokenType = AccessTokenType.Windows.ToString();
			this.IsCompressed = shouldCompress;
			this.ExtensionData = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			this.WindowsAccessToken = new WindowsAccessToken(logonName, authenticationType, clientSecurityContext, this);
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x00034C42 File Offset: 0x00032E42
		private CommonAccessToken(Stream stream)
		{
			this.Deserialize(stream);
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06001D16 RID: 7446 RVA: 0x00034C51 File Offset: 0x00032E51
		// (set) Token: 0x06001D17 RID: 7447 RVA: 0x00034C59 File Offset: 0x00032E59
		public string TokenType { get; private set; }

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06001D18 RID: 7448 RVA: 0x00034C62 File Offset: 0x00032E62
		// (set) Token: 0x06001D19 RID: 7449 RVA: 0x00034C6A File Offset: 0x00032E6A
		[CLSCompliant(false)]
		public ushort Version { get; set; }

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06001D1A RID: 7450 RVA: 0x00034C73 File Offset: 0x00032E73
		// (set) Token: 0x06001D1B RID: 7451 RVA: 0x00034C7B File Offset: 0x00032E7B
		public bool IsCompressed { get; internal set; }

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06001D1C RID: 7452 RVA: 0x00034C84 File Offset: 0x00032E84
		// (set) Token: 0x06001D1D RID: 7453 RVA: 0x00034C8C File Offset: 0x00032E8C
		public Dictionary<string, string> ExtensionData { get; private set; }

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06001D1E RID: 7454 RVA: 0x00034C95 File Offset: 0x00032E95
		// (set) Token: 0x06001D1F RID: 7455 RVA: 0x00034C9D File Offset: 0x00032E9D
		public WindowsAccessToken WindowsAccessToken { get; private set; }

		// Token: 0x06001D20 RID: 7456 RVA: 0x00034CA8 File Offset: 0x00032EA8
		public static CommonAccessToken Deserialize(string token)
		{
			if (token == null)
			{
				throw new ArgumentNullException("token");
			}
			CommonAccessToken result;
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(token)))
				{
					result = new CommonAccessToken(memoryStream);
				}
			}
			catch (FormatException innerException)
			{
				throw new CommonAccessTokenException(0, AuthorizationStrings.InvalidCommonAccessTokenString, innerException);
			}
			return result;
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x00034D10 File Offset: 0x00032F10
		public string Serialize()
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					this.Serialize(binaryWriter);
				}
				result = Convert.ToBase64String(memoryStream.ToArray());
			}
			return result;
		}

		// Token: 0x06001D22 RID: 7458 RVA: 0x00034D74 File Offset: 0x00032F74
		internal void ReadAndValidateFieldType(BinaryReader reader, char fieldType, LocalizedString errorMessage)
		{
			char c = this.BinaryRead<char>(new Func<char>(reader.ReadChar), errorMessage);
			if (c != fieldType)
			{
				throw new CommonAccessTokenException((int)this.Version, errorMessage);
			}
		}

		// Token: 0x06001D23 RID: 7459 RVA: 0x00034DA8 File Offset: 0x00032FA8
		internal T BinaryRead<T>(Func<T> readMethod, LocalizedString errorMessage)
		{
			T result;
			try
			{
				result = readMethod();
			}
			catch (EndOfStreamException innerException)
			{
				throw new CommonAccessTokenException((int)this.Version, errorMessage, innerException);
			}
			return result;
		}

		// Token: 0x06001D24 RID: 7460 RVA: 0x00034DE0 File Offset: 0x00032FE0
		internal void WriteExtensionData(BinaryWriter writer)
		{
			if (this.ExtensionData != null)
			{
				writer.Write('E');
				writer.Write(this.ExtensionData.Count);
				foreach (KeyValuePair<string, string> keyValuePair in this.ExtensionData)
				{
					writer.Write(keyValuePair.Key);
					writer.Write(keyValuePair.Value ?? string.Empty);
				}
			}
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x00034E70 File Offset: 0x00033070
		internal void ReadExtensionData(BinaryReader reader)
		{
			int num = this.BinaryRead<int>(new Func<int>(reader.ReadInt32), AuthorizationStrings.InvalidExtensionDataLength);
			this.ExtensionData = new Dictionary<string, string>(num, StringComparer.OrdinalIgnoreCase);
			for (int i = 0; i < num; i++)
			{
				string text = this.BinaryRead<string>(new Func<string>(reader.ReadString), AuthorizationStrings.InvalidExtensionDataKey);
				if (string.IsNullOrEmpty(text))
				{
					throw new CommonAccessTokenException((int)this.Version, AuthorizationStrings.InvalidExtensionDataKey);
				}
				string value = this.BinaryRead<string>(new Func<string>(reader.ReadString), AuthorizationStrings.InvalidExtensionDataValue);
				this.ExtensionData[text] = value;
			}
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x00034F0C File Offset: 0x0003310C
		private void Serialize(BinaryWriter writer)
		{
			writer.Write('V');
			writer.Write(this.Version);
			writer.Write('T');
			writer.Write(this.TokenType);
			writer.Write('C');
			writer.Write(this.IsCompressed);
			byte[] tokenBytes = this.GetTokenBytes();
			if (this.IsCompressed)
			{
				using (GZipStream gzipStream = new GZipStream(writer.BaseStream, CompressionMode.Compress, true))
				{
					gzipStream.Write(tokenBytes, 0, tokenBytes.Length);
					goto IL_77;
				}
			}
			writer.Write(tokenBytes);
			IL_77:
			writer.Flush();
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x00034FA8 File Offset: 0x000331A8
		private byte[] GetTokenBytes()
		{
			if (!this.TokenType.Equals(AccessTokenType.Windows.ToString(), StringComparison.OrdinalIgnoreCase))
			{
				byte[] result;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
					{
						this.WriteExtensionData(binaryWriter);
						binaryWriter.Flush();
					}
					result = memoryStream.ToArray();
				}
				return result;
			}
			if (this.WindowsAccessToken != null)
			{
				return this.WindowsAccessToken.GetSerializedToken();
			}
			throw new CommonAccessTokenException((int)this.Version, AuthorizationStrings.ExpectingWindowsAccessToken);
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x0003504C File Offset: 0x0003324C
		private void Deserialize(Stream stream)
		{
			using (BinaryReader binaryReader = new BinaryReader(stream))
			{
				this.ReadAndValidateFieldType(binaryReader, 'V', AuthorizationStrings.MissingVersion);
				this.Version = this.BinaryRead<ushort>(new Func<ushort>(binaryReader.ReadUInt16), AuthorizationStrings.MissingVersion);
				this.ReadAndValidateFieldType(binaryReader, 'T', AuthorizationStrings.MissingTokenType);
				this.TokenType = this.BinaryRead<string>(new Func<string>(binaryReader.ReadString), AuthorizationStrings.MissingTokenType);
				this.ReadAndValidateFieldType(binaryReader, 'C', AuthorizationStrings.MissingIsCompressed);
				this.IsCompressed = this.BinaryRead<bool>(new Func<bool>(binaryReader.ReadBoolean), AuthorizationStrings.MissingIsCompressed);
				int num = (int)(binaryReader.BaseStream.Length - binaryReader.BaseStream.Position);
				if (num > 0)
				{
					byte[] array = binaryReader.ReadBytes(num);
					if (this.IsCompressed)
					{
						array = this.Decompress(array);
					}
					if (this.TokenType.Equals(AccessTokenType.Windows.ToString(), StringComparison.OrdinalIgnoreCase))
					{
						this.WindowsAccessToken = new WindowsAccessToken(this);
						this.WindowsAccessToken.DeserializeFromToken(array);
					}
					else
					{
						this.ReadExtensionData(array);
					}
				}
				else if (this.TokenType.Equals(AccessTokenType.Windows.ToString(), StringComparison.OrdinalIgnoreCase))
				{
					throw new CommonAccessTokenException((int)this.Version, AuthorizationStrings.InvalidWindowsAccessToken);
				}
			}
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x000351A8 File Offset: 0x000333A8
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

		// Token: 0x06001D2A RID: 7466 RVA: 0x00035228 File Offset: 0x00033428
		private void ReadExtensionData(byte[] bytes)
		{
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					this.ReadAndValidateFieldType(binaryReader, 'E', AuthorizationStrings.InvalidFieldType);
					this.ReadExtensionData(binaryReader);
				}
			}
		}

		// Token: 0x04001D89 RID: 7561
		public const string HttpHeaderName = "X-CommonAccessToken";

		// Token: 0x04001D8A RID: 7562
		public const string HttpContextItemKey = "Item-CommonAccessToken";

		// Token: 0x04001D8B RID: 7563
		private const ushort CurrentVersion = 1;

		// Token: 0x04001D8C RID: 7564
		private const ushort UnknownVersion = 0;

		// Token: 0x04001D8D RID: 7565
		private static readonly List<string> ValidExtensionDataKeys;
	}
}
