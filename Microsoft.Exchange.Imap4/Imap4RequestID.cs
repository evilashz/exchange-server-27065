using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000028 RID: 40
	internal sealed class Imap4RequestID : Imap4RequestWithStringParameters
	{
		// Token: 0x060001AD RID: 429 RVA: 0x0000B5AE File Offset: 0x000097AE
		public Imap4RequestID(Imap4ResponseFactory factory, string tag, string data) : this(factory, tag, data, 1, 1)
		{
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000B5BB File Offset: 0x000097BB
		public Imap4RequestID(Imap4ResponseFactory factory, string tag, string data, int minNumberOfArguments, int maxNumberOfArguments) : base(factory, tag, data, minNumberOfArguments, maxNumberOfArguments)
		{
			this.PerfCounterTotal = base.Imap4CountersInstance.PerfCounter_ID_Total;
			this.PerfCounterFailures = base.Imap4CountersInstance.PerfCounter_ID_Failures;
			base.AllowedStates = Imap4State.All;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000B5F4 File Offset: 0x000097F4
		public override ProtocolResponse Process()
		{
			string arg = base.Session.Server.ToString();
			string[] array = "15.00.1497.010".Split(new char[]
			{
				'.'
			});
			string input = string.Empty;
			string arg2 = string.Empty;
			try
			{
				arg2 = string.Format("{0}.{1}", int.Parse(array[0]), int.Parse(array[1]));
			}
			catch
			{
				arg2 = "15.00.1497.010";
			}
			Imap4Response.Type imap4ResponseType;
			if (this.CheckParameters())
			{
				input = string.Format("* ID (\"name\" \"{0}\" \"version\" \"{1}\")\r\n{2}", arg, arg2, "[*] ID completed");
				imap4ResponseType = Imap4Response.Type.ok;
			}
			else
			{
				input = "[*] ID failed";
				imap4ResponseType = Imap4Response.Type.bad;
			}
			return new Imap4Response(this, imap4ResponseType, input);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000B6B8 File Offset: 0x000098B8
		private bool CheckParameters()
		{
			try
			{
				if (base.ArrayOfArguments == null || base.ArrayOfArguments.Count != 1)
				{
					return false;
				}
				string text = base.ArrayOfArguments[0];
				if (string.IsNullOrWhiteSpace(text))
				{
					return false;
				}
				if (string.Compare(text, "NIL", StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
				if (!text.StartsWith("(\"") || !text.EndsWith("\")"))
				{
					return false;
				}
				int num = (from c in text.ToCharArray()
				where c == '"'
				select c).Count<char>();
				if (num % 4 != 0 || num > 120)
				{
					return false;
				}
				int num2 = Regex.Split(text, "\" \"", RegexOptions.IgnoreCase).Length;
				if (num2 % 2 != 0 || num2 * 2 != num)
				{
					return false;
				}
				Regex regex = new Regex("(\"(?<fieldName>[^\"]+)\")", RegexOptions.IgnoreCase | RegexOptions.Compiled);
				Match match = regex.Match(text);
				int num3 = 0;
				if (!match.Success)
				{
					return false;
				}
				while (match.Success)
				{
					string value = match.Groups["fieldName"].Value;
					switch (num3++ % 2)
					{
					case 0:
						if (string.IsNullOrWhiteSpace(value) || value.Length > 30)
						{
							return false;
						}
						break;
					case 1:
						if (value.Length > 1024)
						{
							return false;
						}
						break;
					}
					match = match.NextMatch();
				}
				if (num3 * 2 != num)
				{
					return false;
				}
			}
			catch (Exception ex)
			{
				ProtocolBaseServices.SessionTracer.TraceDebug(base.Session.SessionId, ex.ToString());
				return false;
			}
			return true;
		}

		// Token: 0x04000142 RID: 322
		private const string IDResponseCompleted = "[*] ID completed";

		// Token: 0x04000143 RID: 323
		private const string IDResponseFailed = "[*] ID failed";

		// Token: 0x04000144 RID: 324
		private const string IDResponseFormat = "* ID (\"name\" \"{0}\" \"version\" \"{1}\")\r\n{2}";

		// Token: 0x04000145 RID: 325
		private const string IDResponseFormatNil = "* ID NIL\r\n{0}";

		// Token: 0x04000146 RID: 326
		private const string IDNil = "NIL";

		// Token: 0x04000147 RID: 327
		private const string IDParaRegex = "(\"(?<fieldName>[^\"]+)\")";
	}
}
