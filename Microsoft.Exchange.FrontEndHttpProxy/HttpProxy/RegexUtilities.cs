using System;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200009B RID: 155
	internal static class RegexUtilities
	{
		// Token: 0x0600048E RID: 1166 RVA: 0x0001A860 File Offset: 0x00018A60
		internal static Match TryMatch(Regex regex, string data, RequestDetailsLogger logger)
		{
			Match result = null;
			try
			{
				result = regex.Match(data);
			}
			catch (FormatException ex)
			{
				logger.AppendGenericError("Regex Exception", string.Format("Matching regex {0} - {1}", regex.ToString(), ex.ToString()));
			}
			catch (OverflowException ex2)
			{
				logger.AppendGenericError("Regex Exception", string.Format("Matching regex {0} - {1}", regex.ToString(), ex2.ToString()));
			}
			catch (ArgumentException ex3)
			{
				logger.AppendGenericError("Regex Exception", string.Format("Matching regex {0} - {1}", regex.ToString(), ex3.ToString()));
			}
			return result;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0001A910 File Offset: 0x00018B10
		internal static string ParseIdentifier(Match match, string pattern, RequestDetailsLogger logger)
		{
			string result = null;
			try
			{
				result = match.Result(pattern);
			}
			catch (FormatException ex)
			{
				logger.AppendGenericError("Regex Exception", string.Format("Parsing {0} - {1}", match.ToString(), ex.ToString()));
			}
			catch (OverflowException ex2)
			{
				logger.AppendGenericError("Regex Exception", string.Format("Parsing {0} - {1}", match.ToString(), ex2.ToString()));
			}
			catch (ArgumentException ex3)
			{
				logger.AppendGenericError("Regex Exception", string.Format("Parsing {0} - {1}", match.ToString(), ex3.ToString()));
			}
			return result;
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0001A9C0 File Offset: 0x00018BC0
		internal static bool TryGetServerVersionFromRegexMatch(Match match, out ServerVersion version)
		{
			version = null;
			try
			{
				int major = Convert.ToInt32(match.Result("${major}"));
				int minor = Convert.ToInt32(match.Result("${minor}"));
				int build = Convert.ToInt32(match.Result("${build}"));
				int revision = Convert.ToInt32(match.Result("${revision}"));
				version = new ServerVersion(major, minor, build, revision);
				return true;
			}
			catch (FormatException arg)
			{
				ExTraceGlobals.VerboseTracer.TraceError<FormatException>(0L, "[RegexUtilities::TryGetServerVersionFromRegexMatch]: FormatException {0} received when processing version.", arg);
			}
			catch (OverflowException arg2)
			{
				ExTraceGlobals.VerboseTracer.TraceError<OverflowException>(0L, "[RegexUtilities::TryGetServerVersionFromRegexMatch]: OverflowException {0} received when processing version.", arg2);
			}
			catch (ArgumentOutOfRangeException arg3)
			{
				ExTraceGlobals.VerboseTracer.TraceError<ArgumentOutOfRangeException>(0L, "[RegexUtilities::TryGetServerVersionFromRegexMatch]: ArgumentOutOfRangeException {0} received when processing version.", arg3);
			}
			return false;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0001AA98 File Offset: 0x00018C98
		internal static bool TryGetMailboxGuidAddressFromRegexMatch(Match match, out Guid mailboxGuid, out string domain)
		{
			mailboxGuid = Guid.Empty;
			domain = null;
			Group group = match.Groups["hint"];
			if (group != null && SmtpAddress.IsValidSmtpAddress(group.Value))
			{
				SmtpAddress smtpAddress = new SmtpAddress(group.Value);
				if (Guid.TryParse(smtpAddress.Local, out mailboxGuid))
				{
					domain = smtpAddress.Domain;
					return true;
				}
			}
			return false;
		}
	}
}
