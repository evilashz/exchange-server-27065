using System;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x02000007 RID: 7
	internal abstract class Pop3RequestWithIntParameters : Pop3Request
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002D60 File Offset: 0x00000F60
		public Pop3RequestWithIntParameters(ResponseFactory factory, string arguments, Pop3RequestWithIntParameters.ArgumentsTypes argumentsType) : base(factory, arguments)
		{
			this.ArgumentsType = argumentsType;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002D71 File Offset: 0x00000F71
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002D79 File Offset: 0x00000F79
		protected Pop3RequestWithIntParameters.ArgumentsTypes ArgumentsType { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002D82 File Offset: 0x00000F82
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002D8A File Offset: 0x00000F8A
		protected int Lines
		{
			get
			{
				return this.lines;
			}
			set
			{
				this.lines = value;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002D94 File Offset: 0x00000F94
		public override void ParseArguments()
		{
			if (base.Arguments == null)
			{
				if (this.ArgumentsType != Pop3RequestWithIntParameters.ArgumentsTypes.one_optional)
				{
					this.ParseResult = ParseResult.invalidNumberOfArguments;
					return;
				}
				this.idx = -1;
			}
			else
			{
				string[] array = base.Arguments.Trim().Split(ResponseFactory.WordDelimiter);
				if (this.ArgumentsType == Pop3RequestWithIntParameters.ArgumentsTypes.two_mandatory)
				{
					if (array.Length != 2)
					{
						this.ParseResult = ParseResult.invalidNumberOfArguments;
						return;
					}
					if (!int.TryParse(array[0], out this.idx) || this.idx < 1)
					{
						this.ParseResult = ParseResult.invalidArgument;
						return;
					}
					if (!int.TryParse(array[1], out this.lines) || this.lines < 0)
					{
						this.ParseResult = ParseResult.invalidArgument;
						return;
					}
				}
				else
				{
					if (array.Length != 1)
					{
						this.ParseResult = ParseResult.invalidNumberOfArguments;
						return;
					}
					if (!int.TryParse(array[0], out this.idx) || this.idx < 1)
					{
						this.ParseResult = ParseResult.invalidArgument;
						return;
					}
				}
			}
			this.ParseResult = ParseResult.success;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002E6C File Offset: 0x0000106C
		public override ProtocolResponse Process()
		{
			if (this.idx == -1)
			{
				return this.ProcessAllMessages();
			}
			Pop3Message message = base.Factory.GetMessage(this.idx);
			if (message == null)
			{
				return new Pop3Response(Pop3Response.Type.err, "The specified message is out of range.");
			}
			if (message.IsDeleted)
			{
				return new Pop3Response(Pop3Response.Type.err, "The requested message is no longer available; it may have been deleted.");
			}
			return this.ProcessMessage(message);
		}

		// Token: 0x06000030 RID: 48
		public abstract ProtocolResponse ProcessMessage(Pop3Message message);

		// Token: 0x06000031 RID: 49 RVA: 0x00002EC5 File Offset: 0x000010C5
		public virtual ProtocolResponse ProcessAllMessages()
		{
			ProtocolBaseServices.Assert(false, "Pop3RequestWithIntParameters.ProcessAllMessages can't be called directly.", new object[0]);
			return null;
		}

		// Token: 0x0400001C RID: 28
		internal const string ResponseDeleted = "The requested message is no longer available; it may have been deleted.";

		// Token: 0x0400001D RID: 29
		internal const string ResponseDoesNotExist = "The specified message is out of range.";

		// Token: 0x0400001E RID: 30
		private const int OPTIONAL = -1;

		// Token: 0x0400001F RID: 31
		private int idx;

		// Token: 0x04000020 RID: 32
		private int lines;

		// Token: 0x02000008 RID: 8
		internal enum ArgumentsTypes
		{
			// Token: 0x04000023 RID: 35
			one_optional,
			// Token: 0x04000024 RID: 36
			one_mandatory,
			// Token: 0x04000025 RID: 37
			two_mandatory
		}
	}
}
