using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000909 RID: 2313
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class RoutingTypeDriver
	{
		// Token: 0x060056D5 RID: 22229 RVA: 0x00165ED0 File Offset: 0x001640D0
		public static RoutingTypeDriver PickRoutingTypeDriver(string routingType)
		{
			foreach (RoutingTypeDriver routingTypeDriver in RoutingTypeDriver.routingTypeDriverChain)
			{
				if (routingTypeDriver.IsRoutingTypeSupported(routingType))
				{
					return routingTypeDriver;
				}
			}
			throw new Exception("Failed to pick a RoutingTypeDriver for " + routingType);
		}

		// Token: 0x060056D6 RID: 22230 RVA: 0x00165F14 File Offset: 0x00164114
		public static bool TryDetectRoutingType(PropertyBag propertyBag, out RoutingTypeDriver detectedRtDriver, out string routingType)
		{
			detectedRtDriver = null;
			routingType = null;
			foreach (RoutingTypeDriver routingTypeDriver in RoutingTypeDriver.routingTypeDriverChain)
			{
				if (routingTypeDriver.TryDetectRoutingType(propertyBag, out routingType))
				{
					detectedRtDriver = routingTypeDriver;
					return true;
				}
			}
			return false;
		}

		// Token: 0x17001839 RID: 6201
		// (get) Token: 0x060056D7 RID: 22231
		internal abstract IEqualityComparer<IParticipant> AddressEqualityComparer { get; }

		// Token: 0x060056D8 RID: 22232 RVA: 0x00165F54 File Offset: 0x00164154
		internal virtual bool? IsRoutable(string routingType, StoreSession session)
		{
			if (session != null)
			{
				return new bool?(session.SupportedRoutingTypes.Contains(routingType));
			}
			return null;
		}

		// Token: 0x060056D9 RID: 22233
		internal abstract bool IsRoutingTypeSupported(string routingType);

		// Token: 0x060056DA RID: 22234 RVA: 0x00165F7F File Offset: 0x0016417F
		internal virtual bool TryDetectRoutingType(PropertyBag participantPropertyBag, out string routingType)
		{
			routingType = null;
			return false;
		}

		// Token: 0x060056DB RID: 22235
		internal abstract ParticipantValidationStatus Validate(Participant participant);

		// Token: 0x060056DC RID: 22236 RVA: 0x00165F88 File Offset: 0x00164188
		internal virtual void Normalize(PropertyBag participantPropertyBag)
		{
			if (PropertyError.IsPropertyNotFound(participantPropertyBag.TryGetProperty(ParticipantSchema.DisplayName)))
			{
				participantPropertyBag.SetOrDeleteProperty(ParticipantSchema.DisplayName, participantPropertyBag.TryGetProperty(ParticipantSchema.EmailAddress));
			}
			string text = participantPropertyBag.TryGetProperty(ParticipantSchema.DisplayName) as string;
			if (!string.IsNullOrEmpty(text) && text.IndexOfAny(RoutingTypeDriver.BidiMarks) >= 0)
			{
				StringBuilder stringBuilder = new StringBuilder(text.Length);
				bool flag = false;
				int i = 0;
				string text2 = text;
				int j = 0;
				while (j < text2.Length)
				{
					char c = text2[j];
					if (!flag)
					{
						goto IL_94;
					}
					flag = false;
					if (!char.IsHighSurrogate(c))
					{
						goto IL_94;
					}
					stringBuilder.Append(c);
					IL_102:
					j++;
					continue;
					IL_94:
					if (char.IsLowSurrogate(c))
					{
						flag = true;
						stringBuilder.Append(c);
						goto IL_102;
					}
					if (c == '‪' || c == '‫' || c == '‭' || c == '‮')
					{
						i++;
						stringBuilder.Append(c);
						goto IL_102;
					}
					if (c != '‬')
					{
						stringBuilder.Append(c);
						goto IL_102;
					}
					if (i > 0)
					{
						i--;
						stringBuilder.Append(c);
						goto IL_102;
					}
					goto IL_102;
				}
				while (i > 0)
				{
					stringBuilder.Append('‬');
					i--;
				}
				participantPropertyBag.SetProperty(ParticipantSchema.DisplayName, stringBuilder.ToString());
			}
		}

		// Token: 0x060056DD RID: 22237 RVA: 0x001660D2 File Offset: 0x001642D2
		internal virtual string FormatAddress(Participant participant, AddressFormat addressFormat)
		{
			if (addressFormat == AddressFormat.OutlookFormat)
			{
				return string.Format("\"{0}\" [{2}:{1}]", participant.DisplayName, participant.EmailAddress, participant.RoutingType);
			}
			return null;
		}

		// Token: 0x04002E54 RID: 11860
		private const string OutlookFormat = "\"{0}\" [{2}:{1}]";

		// Token: 0x04002E55 RID: 11861
		private const char LRE = '‪';

		// Token: 0x04002E56 RID: 11862
		private const char RLE = '‫';

		// Token: 0x04002E57 RID: 11863
		private const char PDF = '‬';

		// Token: 0x04002E58 RID: 11864
		private const char LRO = '‭';

		// Token: 0x04002E59 RID: 11865
		private const char RLO = '‮';

		// Token: 0x04002E5A RID: 11866
		private static readonly char[] BidiMarks = new char[]
		{
			'‪',
			'‫',
			'‬',
			'‭',
			'‮'
		};

		// Token: 0x04002E5B RID: 11867
		private static readonly RoutingTypeDriver[] routingTypeDriverChain = new RoutingTypeDriver[]
		{
			new ExRoutingTypeDriver(),
			new SmtpRoutingTypeDriver(),
			new DLRoutingTypeDriver(),
			new MobileRoutingTypeDriver(),
			new GenericCustomRoutingTypeDriver(),
			new UnspecifiedRoutingTypeDriver()
		};

		// Token: 0x0200090A RID: 2314
		protected sealed class OrdinalCaseInsensitiveAddressEqualityComparerImpl : IEqualityComparer<IParticipant>, IEqualityComparer<string>
		{
			// Token: 0x060056E0 RID: 22240 RVA: 0x0016616C File Offset: 0x0016436C
			public bool Equals(IParticipant x, IParticipant y)
			{
				if (x.EmailAddress == null)
				{
					return x.Equals(y);
				}
				return this.Equals(x.EmailAddress, y.EmailAddress);
			}

			// Token: 0x060056E1 RID: 22241 RVA: 0x00166190 File Offset: 0x00164390
			public int GetHashCode(IParticipant x)
			{
				if (x.EmailAddress == null)
				{
					return x.GetHashCode();
				}
				return this.GetHashCode(x.EmailAddress);
			}

			// Token: 0x060056E2 RID: 22242 RVA: 0x001661AD File Offset: 0x001643AD
			public bool Equals(string x, string y)
			{
				return StringComparer.OrdinalIgnoreCase.Equals(x, y);
			}

			// Token: 0x060056E3 RID: 22243 RVA: 0x001661BB File Offset: 0x001643BB
			public int GetHashCode(string x)
			{
				return StringComparer.OrdinalIgnoreCase.GetHashCode(x);
			}

			// Token: 0x04002E5C RID: 11868
			public static RoutingTypeDriver.OrdinalCaseInsensitiveAddressEqualityComparerImpl Default = new RoutingTypeDriver.OrdinalCaseInsensitiveAddressEqualityComparerImpl();
		}
	}
}
