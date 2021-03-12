using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002FA RID: 762
	internal sealed class RopLogon : Rop
	{
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x060011A7 RID: 4519 RVA: 0x000308B3 File Offset: 0x0002EAB3
		// (set) Token: 0x060011A8 RID: 4520 RVA: 0x000308BA File Offset: 0x0002EABA
		public static IAuthenticationContextCompression AuthenticationContextCompression { get; set; }

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x060011A9 RID: 4521 RVA: 0x000308C2 File Offset: 0x0002EAC2
		internal override RopId RopId
		{
			get
			{
				return RopId.Logon;
			}
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x000308C9 File Offset: 0x0002EAC9
		internal static Rop CreateRop()
		{
			return new RopLogon();
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x000308D0 File Offset: 0x0002EAD0
		internal void SetInput(byte logonIndex, byte handleTableIndex, LogonFlags logonFlags, OpenFlags openFlags, StoreState storeState, LogonExtendedRequestFlags extendedFlags, MailboxId? mailboxId, LocaleInfo? localeInfo, string applicationId, AuthenticationContext authenticationContext, byte[] tenantHint)
		{
			if ((extendedFlags & LogonExtendedRequestFlags.AuthContextCompressed) != LogonExtendedRequestFlags.None)
			{
				throw new ArgumentException("Compressed authentication context is not supported", "extendedFlags");
			}
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.logonFlags = logonFlags;
			this.openFlags = openFlags;
			this.storeState = storeState;
			this.extendedFlags = extendedFlags;
			this.mailboxId = mailboxId;
			this.localeInfo = localeInfo;
			this.applicationId = applicationId;
			this.authenticationContext = authenticationContext;
			this.tenantHint = tenantHint;
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x00030944 File Offset: 0x0002EB44
		internal override void Execute(IConnectionInformation connection, IRopDriver ropDriver, ServerObjectHandleTable handleTable, ArraySegment<byte> outputBuffer)
		{
			LogonResultFactory resultFactory = new LogonResultFactory(base.LogonIndex);
			ServerObjectHandle value = ServerObjectHandle.None;
			this.result = ropDriver.RopHandler.Logon(this.logonFlags, this.openFlags, this.storeState, this.extendedFlags, this.mailboxId, this.localeInfo, this.applicationId, this.authenticationContext, this.tenantHint, resultFactory);
			if (this.result is SuccessfulLogonResult)
			{
				this.result.String8Encoding = this.result.ReturnObject.String8Encoding;
				ServerObjectMap map = ropDriver.CreateLogon(base.LogonIndex, this.logonFlags);
				value = this.result.GetServerObjectHandle(map);
			}
			else
			{
				this.result.String8Encoding = connection.String8Encoding;
			}
			handleTable[(int)base.HandleTableIndex] = value;
			base.Result.SetServerObjectHandleIndex(base.HandleTableIndex);
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x00030A24 File Offset: 0x0002EC24
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte((byte)this.logonFlags);
			writer.WriteUInt32((uint)this.openFlags);
			writer.WriteUInt32((uint)this.storeState);
			if (this.mailboxId != null)
			{
				writer.WriteUInt16((ushort)this.mailboxId.Value.SerializedLength());
			}
			else
			{
				writer.WriteUInt16(0);
			}
			if (this.IsLogonFlagSet(LogonFlags.Extended))
			{
				writer.WriteUInt32((uint)(this.extendedFlags & ~LogonExtendedRequestFlags.AuthContextCompressed));
			}
			if (this.mailboxId != null)
			{
				this.mailboxId.Value.Serialize(writer);
			}
			if (this.IsExtendedFlagSet(LogonExtendedRequestFlags.UseLocaleInfo))
			{
				this.localeInfo.Value.Serialize(writer);
			}
			if (this.IsExtendedFlagSet(LogonExtendedRequestFlags.ApplicationId))
			{
				writer.WriteAsciiString(this.applicationId, StringFlags.Sized16);
			}
			if (this.IsExtendedFlagSet(LogonExtendedRequestFlags.SetAuthContext))
			{
				this.authenticationContext.Serialize(writer);
			}
			if (this.IsExtendedFlagSet(LogonExtendedRequestFlags.TenantHint))
			{
				writer.WriteSizedBytes(this.tenantHint);
			}
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x00030B25 File Offset: 0x0002ED25
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = LogonResult.Parse(reader);
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x00030B3B File Offset: 0x0002ED3B
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new LogonResultFactory(base.LogonIndex);
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x00030B48 File Offset: 0x0002ED48
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable, IParseLogonTracker logonTracker)
		{
			base.InternalParseInput(reader, serverObjectHandleTable, logonTracker);
			this.logonFlags = (LogonFlags)reader.ReadByte();
			this.openFlags = (OpenFlags)reader.ReadUInt32();
			this.storeState = (StoreState)reader.ReadUInt32();
			ushort num = reader.ReadUInt16();
			if (this.IsLogonFlagSet(LogonFlags.Extended))
			{
				this.extendedFlags = (LogonExtendedRequestFlags)reader.ReadUInt32();
			}
			if (this.IsLogonFlagSet(LogonFlags.MbxGuids))
			{
				if (num != 32)
				{
					throw new BufferParseException("Incorrect size specified for guids");
				}
				Guid mailboxGuid = reader.ReadGuid();
				Guid databaseGuid = reader.ReadGuid();
				this.mailboxId = new MailboxId?(new MailboxId(mailboxGuid, databaseGuid));
			}
			else if (num > 0)
			{
				long position = reader.Position;
				string mailboxLegacyDn = reader.ReadAsciiString(StringFlags.IncludeNull);
				if (reader.Position - position > (long)((ulong)num))
				{
					throw new BufferParseException("Mailbox Legacy DN actual size larger than specified size");
				}
				reader.Position = position + (long)((ulong)num);
				this.mailboxId = new MailboxId?(new MailboxId(mailboxLegacyDn));
			}
			if (this.IsExtendedFlagSet(LogonExtendedRequestFlags.UseLocaleInfo))
			{
				this.localeInfo = new LocaleInfo?(LocaleInfo.Parse(reader));
			}
			if (this.IsExtendedFlagSet(LogonExtendedRequestFlags.ApplicationId))
			{
				this.applicationId = reader.ReadAsciiString(StringFlags.Sized16);
			}
			if (this.IsExtendedFlagSet(LogonExtendedRequestFlags.SetAuthContext))
			{
				Reader reader2 = reader;
				using (DisposeGuard disposeGuard = default(DisposeGuard))
				{
					if (this.IsExtendedFlagSet(LogonExtendedRequestFlags.AuthContextCompressed))
					{
						reader2 = RopLogon.GetDecompressedDataReader(reader);
						disposeGuard.Add<Reader>(reader2);
					}
					this.authenticationContext = AuthenticationContext.Parse(reader2);
					this.extendedFlags &= ~LogonExtendedRequestFlags.AuthContextCompressed;
				}
			}
			if (this.IsExtendedFlagSet(LogonExtendedRequestFlags.TenantHint))
			{
				this.tenantHint = reader.ReadSizeAndByteArray();
			}
			logonTracker.ParseRecordLogon(base.LogonIndex, base.HandleTableIndex, this.logonFlags);
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x00030CEC File Offset: 0x0002EEEC
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x00030D04 File Offset: 0x0002EF04
		private static Reader GetDecompressedDataReader(Reader reader)
		{
			IAuthenticationContextCompression authenticationContextCompression = RopLogon.AuthenticationContextCompression;
			if (authenticationContextCompression == null)
			{
				throw new BufferParseException("Compressed AuthenticationContext is not supported.");
			}
			long position = reader.Position;
			uint num = reader.ReadUInt32();
			if (num > 65535U)
			{
				throw new BufferParseException("Invalid size of the compressed AuthenticationContext buffer.");
			}
			byte[] array = new byte[num];
			uint num2;
			uint num3;
			for (num2 = 0U; num2 < num; num2 += num3)
			{
				uint count = reader.ReadUInt32();
				num3 = reader.ReadUInt32();
				if (num3 > 32768U)
				{
					throw new BufferParseException("Invalid size of the compressed chunk in AuthenticationContext buffer.");
				}
				if (num3 > num - num2)
				{
					throw new BufferParseException("Invalid uncompressed size of the compressed chunk in AuthenticationContext buffer.");
				}
				if (!authenticationContextCompression.TryDecompress(reader.ReadArraySegment(count), new ArraySegment<byte>(array, (int)num2, (int)num3)))
				{
					throw new BufferParseException("Failed to decompress a compressed chunk in AuthenticationContext buffer.");
				}
			}
			if (num2 != num)
			{
				throw new BufferParseException("Actual size of uncompressed data does not match the declared size in the AuthenticationContext buffer.");
			}
			return Reader.CreateBufferReader(array);
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x00030DCE File Offset: 0x0002EFCE
		private bool IsLogonFlagSet(LogonFlags flagToTest)
		{
			return (this.logonFlags & flagToTest) == flagToTest;
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x00030DDB File Offset: 0x0002EFDB
		private bool IsExtendedFlagSet(LogonExtendedRequestFlags flagToTest)
		{
			return this.IsLogonFlagSet(LogonFlags.Extended) && (this.extendedFlags & flagToTest) == flagToTest;
		}

		// Token: 0x04000996 RID: 2454
		private const int MaxChunkSize = 32768;

		// Token: 0x04000997 RID: 2455
		private const RopId RopType = RopId.Logon;

		// Token: 0x04000998 RID: 2456
		private LogonFlags logonFlags;

		// Token: 0x04000999 RID: 2457
		private OpenFlags openFlags;

		// Token: 0x0400099A RID: 2458
		private StoreState storeState;

		// Token: 0x0400099B RID: 2459
		private LogonExtendedRequestFlags extendedFlags;

		// Token: 0x0400099C RID: 2460
		private MailboxId? mailboxId;

		// Token: 0x0400099D RID: 2461
		private LocaleInfo? localeInfo;

		// Token: 0x0400099E RID: 2462
		private string applicationId;

		// Token: 0x0400099F RID: 2463
		private AuthenticationContext authenticationContext;

		// Token: 0x040009A0 RID: 2464
		private byte[] tenantHint;
	}
}
