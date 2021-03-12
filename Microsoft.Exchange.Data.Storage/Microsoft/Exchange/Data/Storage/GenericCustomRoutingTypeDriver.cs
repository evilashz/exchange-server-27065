using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000911 RID: 2321
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class GenericCustomRoutingTypeDriver : RoutingTypeDriver
	{
		// Token: 0x1700183E RID: 6206
		// (get) Token: 0x06005712 RID: 22290 RVA: 0x001666B2 File Offset: 0x001648B2
		internal override IEqualityComparer<IParticipant> AddressEqualityComparer
		{
			get
			{
				return GenericCustomRoutingTypeDriver.AddressEqualityComparerImpl.Default;
			}
		}

		// Token: 0x06005713 RID: 22291 RVA: 0x001666BC File Offset: 0x001648BC
		internal static List<PropValue> TryParseOutlookFormat(string inputString)
		{
			if (inputString.Length == 0 || inputString.Length > 9879)
			{
				return null;
			}
			Match match = GenericCustomRoutingTypeDriver.outlookParserRegex.Value.Match(inputString);
			if (!match.Success || match.Groups["addr"].Captures.Count != 1)
			{
				return null;
			}
			string value = match.Groups["addr"].Value;
			string text = match.Groups["rt"].Value;
			if (string.IsNullOrEmpty(text))
			{
				if (!SmtpAddress.IsValidSmtpAddress(value))
				{
					return null;
				}
				text = "SMTP";
			}
			return Participant.ListCoreProperties(match.Groups["dn"].Value.TrimEnd(new char[0]), value, text);
		}

		// Token: 0x06005714 RID: 22292 RVA: 0x00166785 File Offset: 0x00164985
		internal override bool IsRoutingTypeSupported(string routingType)
		{
			return routingType != null;
		}

		// Token: 0x06005715 RID: 22293 RVA: 0x0016678E File Offset: 0x0016498E
		internal override bool TryDetectRoutingType(PropertyBag participantPropertyBag, out string routingType)
		{
			routingType = null;
			return false;
		}

		// Token: 0x06005716 RID: 22294 RVA: 0x00166794 File Offset: 0x00164994
		internal override ParticipantValidationStatus Validate(Participant participant)
		{
			if (participant.EmailAddress == null)
			{
				return ParticipantValidationStatus.AddressRequiredForRoutingType;
			}
			return ParticipantValidationStatus.NoError;
		}

		// Token: 0x06005717 RID: 22295 RVA: 0x001667A4 File Offset: 0x001649A4
		private static Regex LoadOutlookParserRegex()
		{
			Regex result;
			using (Stream manifestResourceStream = typeof(GenericCustomRoutingTypeDriver).GetTypeInfo().Assembly.GetManifestResourceStream("OutlookParser.re.txt"))
			{
				using (StreamReader streamReader = new StreamReader(manifestResourceStream, true))
				{
					result = new Regex(streamReader.ReadToEnd(), RegexOptions.Compiled);
				}
			}
			return result;
		}

		// Token: 0x04002E63 RID: 11875
		private const int MaxVariousDelimitersLength = 10;

		// Token: 0x04002E64 RID: 11876
		private static LazilyInitialized<Regex> outlookParserRegex = new LazilyInitialized<Regex>(new Func<Regex>(GenericCustomRoutingTypeDriver.LoadOutlookParserRegex));

		// Token: 0x02000912 RID: 2322
		private sealed class AddressEqualityComparerImpl : IEqualityComparer<IParticipant>, IEqualityComparer<string>
		{
			// Token: 0x0600571A RID: 22298 RVA: 0x00166838 File Offset: 0x00164A38
			public bool Equals(IParticipant x, IParticipant y)
			{
				if (x.EmailAddress == null)
				{
					return x.Equals(y);
				}
				return StringComparer.Ordinal.Equals(x.RoutingType, y.RoutingType) && this.Equals(x.EmailAddress, y.EmailAddress);
			}

			// Token: 0x0600571B RID: 22299 RVA: 0x00166876 File Offset: 0x00164A76
			public int GetHashCode(IParticipant x)
			{
				if (x.EmailAddress == null)
				{
					return x.GetHashCode();
				}
				return StringComparer.Ordinal.GetHashCode(x.RoutingType) ^ this.GetHashCode(x.EmailAddress);
			}

			// Token: 0x0600571C RID: 22300 RVA: 0x001668A4 File Offset: 0x00164AA4
			public bool Equals(string x, string y)
			{
				return StringComparer.Ordinal.Equals(x, y);
			}

			// Token: 0x0600571D RID: 22301 RVA: 0x001668B2 File Offset: 0x00164AB2
			public int GetHashCode(string x)
			{
				return StringComparer.Ordinal.GetHashCode(x);
			}

			// Token: 0x04002E65 RID: 11877
			public static GenericCustomRoutingTypeDriver.AddressEqualityComparerImpl Default = new GenericCustomRoutingTypeDriver.AddressEqualityComparerImpl();
		}
	}
}
