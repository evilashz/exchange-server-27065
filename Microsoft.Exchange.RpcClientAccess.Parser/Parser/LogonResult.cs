using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002FB RID: 763
	internal abstract class LogonResult : RopResult
	{
		// Token: 0x060011B6 RID: 4534 RVA: 0x00030DFC File Offset: 0x0002EFFC
		protected LogonResult(ErrorCode errorCode, IServerObject returnObject) : base(RopId.Logon, errorCode, returnObject)
		{
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x00030E0B File Offset: 0x0002F00B
		protected LogonResult(Reader reader) : base(reader)
		{
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00030E14 File Offset: 0x0002F014
		public static RopResult Parse(Reader reader)
		{
			ErrorCode errorCode = (ErrorCode)reader.PeekUInt32(2L);
			ErrorCode errorCode2 = errorCode;
			if (errorCode2 != ErrorCode.None)
			{
				if (errorCode2 == ErrorCode.WrongServer)
				{
					return new RedirectLogonResult(reader);
				}
				return StandardRopResult.ParseFailResult(reader);
			}
			else
			{
				LogonFlags logonFlags = (LogonFlags)reader.PeekByte(6L);
				if ((byte)(logonFlags & LogonFlags.Private) == 1)
				{
					return new SuccessfulPrivateLogonResult(reader);
				}
				return new SuccessfulPublicLogonResult(reader);
			}
		}
	}
}
