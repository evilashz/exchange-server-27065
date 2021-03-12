using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020008F4 RID: 2292
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class Participant : IParticipant, IEquatable<IParticipant>, IReadOnlyPropertyBag, IEquatable<Participant>
	{
		// Token: 0x060055EB RID: 21995 RVA: 0x0016318C File Offset: 0x0016138C
		public Participant(string displayName, string emailAddress, string routingType) : this(displayName, emailAddress, routingType, null, Array<PropValue>.Empty)
		{
		}

		// Token: 0x060055EC RID: 21996 RVA: 0x0016319D File Offset: 0x0016139D
		public Participant(string displayName, string emailAddress, string routingType, string originalDisplayName) : this(displayName, emailAddress, routingType, originalDisplayName, null, Array<PropValue>.Empty)
		{
		}

		// Token: 0x060055ED RID: 21997 RVA: 0x001631B0 File Offset: 0x001613B0
		public Participant(string displayName, string emailAddress, string routingType, ParticipantOrigin origin, params KeyValuePair<PropertyDefinition, object>[] additionalProperties) : this(displayName, emailAddress, routingType, origin, PropValue.ConvertEnumerator<PropertyDefinition>(additionalProperties))
		{
		}

		// Token: 0x060055EE RID: 21998 RVA: 0x001631C4 File Offset: 0x001613C4
		public Participant(string displayName, string emailAddress, string routingType, string originalDisplayName, ParticipantOrigin origin, params KeyValuePair<PropertyDefinition, object>[] additionalProperties) : this(displayName, emailAddress, routingType, originalDisplayName, origin, PropValue.ConvertEnumerator<PropertyDefinition>(additionalProperties))
		{
		}

		// Token: 0x060055EF RID: 21999 RVA: 0x001631DA File Offset: 0x001613DA
		public Participant(ADRawEntry adEntry) : this((string)adEntry[ADRecipientSchema.DisplayName], (string)adEntry[ADRecipientSchema.LegacyExchangeDN], "EX", new DirectoryParticipantOrigin(adEntry), new KeyValuePair<PropertyDefinition, object>[0])
		{
		}

		// Token: 0x060055F0 RID: 22000 RVA: 0x00163213 File Offset: 0x00161413
		public Participant(IExchangePrincipal principal) : this(Participant.GetDisplayName(principal), principal.LegacyDn, "EX", new DirectoryParticipantOrigin(principal), new KeyValuePair<PropertyDefinition, object>[0])
		{
		}

		// Token: 0x060055F1 RID: 22001 RVA: 0x00163238 File Offset: 0x00161438
		internal Participant(string displayName, string emailAddress, string routingType, string originalDisplayName, ParticipantOrigin origin, IEnumerable<PropValue> additionalProperties) : this(origin, Util.CompositeEnumerator<PropValue>(new IEnumerable<PropValue>[]
		{
			Participant.ListCoreProperties(displayName, emailAddress, routingType, originalDisplayName),
			additionalProperties
		}))
		{
			foreach (PropValue propValue in additionalProperties)
			{
				if (propValue.Property.Equals(ParticipantSchema.DisplayName) || propValue.Property.Equals(ParticipantSchema.EmailAddress) || propValue.Property.Equals(ParticipantSchema.RoutingType) || propValue.Property.Equals(ParticipantSchema.OriginalDisplayName))
				{
					throw new ArgumentException();
				}
			}
		}

		// Token: 0x060055F2 RID: 22002 RVA: 0x001632F4 File Offset: 0x001614F4
		internal Participant(string displayName, string emailAddress, string routingType, ParticipantOrigin origin, IEnumerable<PropValue> additionalProperties) : this(origin, Util.CompositeEnumerator<PropValue>(new IEnumerable<PropValue>[]
		{
			Participant.ListCoreProperties(displayName, emailAddress, routingType),
			additionalProperties
		}))
		{
			foreach (PropValue propValue in additionalProperties)
			{
				if (propValue.Property.Equals(ParticipantSchema.DisplayName) || propValue.Property.Equals(ParticipantSchema.EmailAddress) || propValue.Property.Equals(ParticipantSchema.RoutingType))
				{
					throw new ArgumentException();
				}
			}
		}

		// Token: 0x060055F3 RID: 22003 RVA: 0x0016339C File Offset: 0x0016159C
		private Participant(ParticipantOrigin origin, Participant.ParticipantPropertyBag propertyBag)
		{
			this.hashCode = new LazilyInitialized<int>(new Func<int>(this.ComputeHashCode));
			this.validationStatus = new LazilyInitialized<ParticipantValidationStatus>(new Func<ParticipantValidationStatus>(this.InternalValidate));
			this.propertyBag = propertyBag;
			this.NormalizeCoreProperties();
			this.origin = (origin ?? Participant.InferOrigin(this.propertyBag));
			this.propertyBag.SetDefaultProperties(this.origin.GetProperties() ?? ((IEnumerable<PropValue>)Array<PropValue>.Empty));
			this.routingTypeDriver = this.SelectRoutingTypeDriver();
			this.routingTypeDriver.Normalize(this.propertyBag);
			this.propertyBag.Freeze();
		}

		// Token: 0x060055F4 RID: 22004 RVA: 0x0016344C File Offset: 0x0016164C
		private Participant(ParticipantOrigin origin, IEnumerable<PropValue> allProperties) : this(origin, new Participant.ParticipantPropertyBag(allProperties))
		{
		}

		// Token: 0x060055F5 RID: 22005 RVA: 0x0016345C File Offset: 0x0016165C
		private static string GetDisplayName(IExchangePrincipal principal)
		{
			RemoteUserMailboxPrincipal remoteUserMailboxPrincipal = principal as RemoteUserMailboxPrincipal;
			if (remoteUserMailboxPrincipal != null)
			{
				return remoteUserMailboxPrincipal.DisplayName;
			}
			return principal.MailboxInfo.DisplayName;
		}

		// Token: 0x17001818 RID: 6168
		// (get) Token: 0x060055F6 RID: 22006 RVA: 0x00163485 File Offset: 0x00161685
		public static IEqualityComparer<Participant> AddressEqualityComparer
		{
			get
			{
				return ParticipantComparer.EmailAddress;
			}
		}

		// Token: 0x17001819 RID: 6169
		// (get) Token: 0x060055F7 RID: 22007 RVA: 0x0016348C File Offset: 0x0016168C
		public IEqualityComparer<IParticipant> EmailAddressEqualityComparer
		{
			get
			{
				return this.routingTypeDriver.AddressEqualityComparer;
			}
		}

		// Token: 0x1700181A RID: 6170
		// (get) Token: 0x060055F8 RID: 22008 RVA: 0x00163499 File Offset: 0x00161699
		// (set) Token: 0x060055F9 RID: 22009 RVA: 0x001634AB File Offset: 0x001616AB
		public string DisplayName
		{
			get
			{
				return this.propertyBag.GetValueOrDefault<string>(ParticipantSchema.DisplayName);
			}
			private set
			{
				this.propertyBag.SetOrDeleteProperty(ParticipantSchema.DisplayName, value);
			}
		}

		// Token: 0x1700181B RID: 6171
		// (get) Token: 0x060055FA RID: 22010 RVA: 0x001634BE File Offset: 0x001616BE
		// (set) Token: 0x060055FB RID: 22011 RVA: 0x001634D0 File Offset: 0x001616D0
		public string OriginalDisplayName
		{
			get
			{
				return this.propertyBag.GetValueOrDefault<string>(ParticipantSchema.OriginalDisplayName);
			}
			private set
			{
				this.propertyBag.SetOrDeleteProperty(ParticipantSchema.OriginalDisplayName, value);
			}
		}

		// Token: 0x1700181C RID: 6172
		// (get) Token: 0x060055FC RID: 22012 RVA: 0x001634E3 File Offset: 0x001616E3
		// (set) Token: 0x060055FD RID: 22013 RVA: 0x001634F5 File Offset: 0x001616F5
		public string EmailAddress
		{
			get
			{
				return this.propertyBag.GetValueOrDefault<string>(ParticipantSchema.EmailAddress);
			}
			private set
			{
				this.propertyBag.SetOrDeleteProperty(ParticipantSchema.EmailAddress, value);
			}
		}

		// Token: 0x1700181D RID: 6173
		// (get) Token: 0x060055FE RID: 22014 RVA: 0x00163508 File Offset: 0x00161708
		// (set) Token: 0x060055FF RID: 22015 RVA: 0x0016351A File Offset: 0x0016171A
		public string SmtpEmailAddress
		{
			get
			{
				return this.propertyBag.GetValueOrDefault<string>(ParticipantSchema.SmtpAddress);
			}
			private set
			{
				this.propertyBag.SetOrDeleteProperty(ParticipantSchema.SmtpAddress, value);
			}
		}

		// Token: 0x1700181E RID: 6174
		// (get) Token: 0x06005600 RID: 22016 RVA: 0x0016352D File Offset: 0x0016172D
		// (set) Token: 0x06005601 RID: 22017 RVA: 0x0016353F File Offset: 0x0016173F
		public string SipUri
		{
			get
			{
				return this.propertyBag.GetValueOrDefault<string>(ParticipantSchema.SipUri);
			}
			private set
			{
				this.propertyBag.SetOrDeleteProperty(ParticipantSchema.SipUri, value);
			}
		}

		// Token: 0x1700181F RID: 6175
		// (get) Token: 0x06005602 RID: 22018 RVA: 0x00163552 File Offset: 0x00161752
		public ParticipantOrigin Origin
		{
			get
			{
				return this.origin;
			}
		}

		// Token: 0x17001820 RID: 6176
		// (get) Token: 0x06005603 RID: 22019 RVA: 0x0016355A File Offset: 0x0016175A
		// (set) Token: 0x06005604 RID: 22020 RVA: 0x0016356C File Offset: 0x0016176C
		public string RoutingType
		{
			get
			{
				return this.propertyBag.GetValueOrDefault<string>(ParticipantSchema.RoutingType);
			}
			private set
			{
				this.propertyBag.SetOrDeleteProperty(ParticipantSchema.RoutingType, value);
			}
		}

		// Token: 0x17001821 RID: 6177
		// (get) Token: 0x06005605 RID: 22021 RVA: 0x0016357F File Offset: 0x0016177F
		public ParticipantValidationStatus ValidationStatus
		{
			get
			{
				return this.validationStatus;
			}
		}

		// Token: 0x17001822 RID: 6178
		// (get) Token: 0x06005606 RID: 22022 RVA: 0x0016358C File Offset: 0x0016178C
		public ICollection<PropertyDefinition> LoadedProperties
		{
			get
			{
				return this.propertyBag.Keys;
			}
		}

		// Token: 0x17001823 RID: 6179
		public object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				return this.propertyBag[propertyDefinition];
			}
		}

		// Token: 0x17001824 RID: 6180
		// (get) Token: 0x06005608 RID: 22024 RVA: 0x001635A7 File Offset: 0x001617A7
		// (set) Token: 0x06005609 RID: 22025 RVA: 0x001635AF File Offset: 0x001617AF
		public bool Submitted { get; set; }

		// Token: 0x0600560A RID: 22026 RVA: 0x001635B8 File Offset: 0x001617B8
		public static Participant Parse(string inputString)
		{
			Participant result;
			bool flag = Participant.TryParse(inputString, out result);
			if (flag)
			{
				return result;
			}
			throw new InvalidParticipantException(ServerStrings.CantParseParticipant(inputString), ParticipantValidationStatus.InvalidAddressFormat);
		}

		// Token: 0x0600560B RID: 22027 RVA: 0x001635E0 File Offset: 0x001617E0
		public static bool TryParse(string inputString, out Participant participant)
		{
			if (inputString == null)
			{
				throw new ArgumentNullException("inputString");
			}
			foreach (Participant.TryParseHandler tryParseHandler in Participant.parsingChain)
			{
				IEnumerable<PropValue> enumerable = tryParseHandler(inputString);
				if (enumerable != null)
				{
					participant = new Participant(null, enumerable);
					return true;
				}
			}
			participant = null;
			return false;
		}

		// Token: 0x0600560C RID: 22028 RVA: 0x00163636 File Offset: 0x00161836
		public static bool operator ==(Participant v1, Participant v2)
		{
			return object.Equals(v1, v2);
		}

		// Token: 0x0600560D RID: 22029 RVA: 0x0016363F File Offset: 0x0016183F
		public static bool operator !=(Participant v1, Participant v2)
		{
			return !object.Equals(v1, v2);
		}

		// Token: 0x0600560E RID: 22030 RVA: 0x0016364B File Offset: 0x0016184B
		public static bool? IsRoutable(string routingType, StoreSession session)
		{
			routingType = Participant.NormalizeRoutingType(routingType);
			return RoutingTypeDriver.PickRoutingTypeDriver(routingType).IsRoutable(routingType, session);
		}

		// Token: 0x0600560F RID: 22031 RVA: 0x00163662 File Offset: 0x00161862
		public static bool RoutingTypeEquals(string routingType1, string routingType2)
		{
			return (string.IsNullOrEmpty(routingType1) && string.IsNullOrEmpty(routingType2)) || string.Equals(routingType1, routingType2, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06005610 RID: 22032 RVA: 0x0016367E File Offset: 0x0016187E
		public static Participant[] TryConvertTo(Participant[] sources, string destinationRoutingType, IRecipientSession adRecipientSession)
		{
			if (adRecipientSession == null)
			{
				throw new ArgumentNullException("adRecipientSession");
			}
			return Participant.InternalTryConvertTo(sources, adRecipientSession.SearchRoot, adRecipientSession.SessionSettings, destinationRoutingType, adRecipientSession.GetHashCode());
		}

		// Token: 0x06005611 RID: 22033 RVA: 0x001636A7 File Offset: 0x001618A7
		public static Participant[] TryConvertTo(Participant[] sources, string destinationRoutingType, MailboxSession session)
		{
			return Participant.InternalTryConvertTo(sources, null, Participant.BatchBuilder.GetADSessionSettings(session), destinationRoutingType, (session != null) ? session.GetHashCode() : 0);
		}

		// Token: 0x06005612 RID: 22034 RVA: 0x001636C3 File Offset: 0x001618C3
		public static bool HasSameEmail(Participant participant1, Participant participant2)
		{
			return Participant.HasSameEmail(participant1, participant2, null, true);
		}

		// Token: 0x06005613 RID: 22035 RVA: 0x001636CE File Offset: 0x001618CE
		public static bool HasSameEmail(Participant participant1, Participant participant2, bool canLookup)
		{
			return Participant.HasSameEmail(participant1, participant2, null, canLookup);
		}

		// Token: 0x06005614 RID: 22036 RVA: 0x001636D9 File Offset: 0x001618D9
		public static bool HasSameEmail(Participant participant1, Participant participant2, MailboxSession session, bool canLookup)
		{
			return Participant.InternalHasSameEmail(participant1, participant2, null, canLookup ? Participant.BatchBuilder.GetADSessionSettings(session) : null, canLookup);
		}

		// Token: 0x06005615 RID: 22037 RVA: 0x001636F0 File Offset: 0x001618F0
		public static bool HasSameEmail(Participant participant1, Participant participant2, IRecipientSession adRecipientSession)
		{
			if (adRecipientSession == null)
			{
				throw new ArgumentNullException("adRecipientSession");
			}
			return Participant.InternalHasSameEmail(participant1, participant2, adRecipientSession.SearchRoot, adRecipientSession.SessionSettings, true);
		}

		// Token: 0x06005616 RID: 22038 RVA: 0x00163714 File Offset: 0x00161914
		public static bool HasSameEmail(Participant participant1, Participant participant2, IExchangePrincipal scopingPrincipal)
		{
			if (scopingPrincipal == null)
			{
				throw new ArgumentNullException("scopingPrincipal");
			}
			return Participant.InternalHasSameEmail(participant1, participant2, null, scopingPrincipal.MailboxInfo.OrganizationId.ToADSessionSettings(), true);
		}

		// Token: 0x06005617 RID: 22039 RVA: 0x0016373D File Offset: 0x0016193D
		public static Participant TryConvertTo(Participant source, string destinationRoutingType)
		{
			return Participant.TryConvertTo(source, destinationRoutingType, true);
		}

		// Token: 0x06005618 RID: 22040 RVA: 0x00163748 File Offset: 0x00161948
		public static Participant TryConvertTo(Participant source, string destinationRoutingType, bool canLookup)
		{
			return Participant.TryConvertTo(new Participant[]
			{
				source
			}, destinationRoutingType, null)[0];
		}

		// Token: 0x06005619 RID: 22041 RVA: 0x0016376C File Offset: 0x0016196C
		public static bool TryGetParticipantsFromDisplayNameProperty(IStorePropertyBag propertyBag, StorePropertyDefinition displayNamePropertyDefinitions, out IList<string> displayNames)
		{
			string valueOrDefault = propertyBag.GetValueOrDefault<string>(displayNamePropertyDefinitions, null);
			return Participant.TryGetParticipantsFromDisplayNameProperty(valueOrDefault, out displayNames);
		}

		// Token: 0x0600561A RID: 22042 RVA: 0x0016378C File Offset: 0x0016198C
		public static bool TryGetParticipantsFromDisplayNameProperty(PropertyBag.BasicPropertyStore propertyBag, AtomicStorePropertyDefinition displayNamePropertyDefinitions, out IList<string> displayNames)
		{
			string displayString = propertyBag.GetValue(displayNamePropertyDefinitions) as string;
			return Participant.TryGetParticipantsFromDisplayNameProperty(displayString, out displayNames);
		}

		// Token: 0x0600561B RID: 22043 RVA: 0x001637B0 File Offset: 0x001619B0
		public static Participant TryConvertTo(Participant source, string destinationRoutingType, MailboxSession session)
		{
			return Participant.TryConvertTo(new Participant[]
			{
				source
			}, destinationRoutingType, session)[0];
		}

		// Token: 0x0600561C RID: 22044 RVA: 0x001637D4 File Offset: 0x001619D4
		private static bool TryGetParticipantsFromDisplayNameProperty(string displayString, out IList<string> displayNames)
		{
			displayNames = new List<string>();
			if (displayString == null)
			{
				return true;
			}
			if (displayString.Length >= 255)
			{
				return false;
			}
			foreach (string text in displayString.Split(Participant.DisplayNamesSeparator, StringSplitOptions.RemoveEmptyEntries))
			{
				displayNames.Add(text.Trim());
			}
			return true;
		}

		// Token: 0x0600561D RID: 22045 RVA: 0x0016382C File Offset: 0x00161A2C
		public bool TryGetADRecipient(IRecipientSession session, out ADRecipient adRecipient)
		{
			Util.ThrowOnNullArgument(session, "session");
			if (string.IsNullOrEmpty(this.RoutingType) || string.IsNullOrEmpty(this.EmailAddress))
			{
				adRecipient = null;
				return false;
			}
			ProxyAddress proxyAddress = ProxyAddress.Parse(this.RoutingType, this.EmailAddress);
			if (proxyAddress is InvalidProxyAddress)
			{
				adRecipient = null;
				return false;
			}
			return ADRecipient.TryGetFromProxyAddress(proxyAddress, session, out adRecipient);
		}

		// Token: 0x0600561E RID: 22046 RVA: 0x0016388A File Offset: 0x00161A8A
		public void Validate()
		{
			if (this.ValidationStatus != ParticipantValidationStatus.NoError)
			{
				throw new InvalidParticipantException(this.GetValidationMessage(), this.ValidationStatus);
			}
		}

		// Token: 0x0600561F RID: 22047 RVA: 0x001638A6 File Offset: 0x00161AA6
		public bool AreAddressesEqual(Participant v)
		{
			return ParticipantComparer.EmailAddress.Equals(this, v);
		}

		// Token: 0x06005620 RID: 22048 RVA: 0x001638B4 File Offset: 0x00161AB4
		public bool Equals(Participant other)
		{
			return this.Equals(other);
		}

		// Token: 0x06005621 RID: 22049 RVA: 0x001638C0 File Offset: 0x00161AC0
		public bool Equals(IParticipant other)
		{
			if (object.ReferenceEquals(other, null) || this.hashCode != other.GetHashCode())
			{
				return false;
			}
			if (object.ReferenceEquals(this, other))
			{
				return true;
			}
			foreach (PropertyDefinition propertyDefinition in this.LoadedProperties)
			{
				object valueOrDefault = other.GetValueOrDefault<object>(propertyDefinition);
				if (!Util.ValueEquals(this.GetValueOrDefault<object>(propertyDefinition), valueOrDefault))
				{
					return false;
				}
			}
			foreach (PropertyDefinition propertyDefinition2 in other.LoadedProperties)
			{
				object valueOrDefault2 = other.GetValueOrDefault<object>(propertyDefinition2);
				if (!PropertyError.IsPropertyNotFound(valueOrDefault2) && !Util.ValueEquals(this.GetValueOrDefault<object>(propertyDefinition2), valueOrDefault2))
				{
					return false;
				}
			}
			return this.Submitted == other.Submitted;
		}

		// Token: 0x06005622 RID: 22050 RVA: 0x001639C4 File Offset: 0x00161BC4
		public override bool Equals(object o)
		{
			return this.Equals(o as Participant);
		}

		// Token: 0x06005623 RID: 22051 RVA: 0x001639D2 File Offset: 0x00161BD2
		public override int GetHashCode()
		{
			return this.hashCode;
		}

		// Token: 0x06005624 RID: 22052 RVA: 0x001639DF File Offset: 0x00161BDF
		public bool? IsRoutable(StoreSession session)
		{
			if (this.ValidationStatus == ParticipantValidationStatus.NoError)
			{
				return this.routingTypeDriver.IsRoutable(this.RoutingType, session);
			}
			return new bool?(false);
		}

		// Token: 0x06005625 RID: 22053 RVA: 0x00163A02 File Offset: 0x00161C02
		public override string ToString()
		{
			return this.ToString(AddressFormat.Verbose);
		}

		// Token: 0x06005626 RID: 22054 RVA: 0x00163A0C File Offset: 0x00161C0C
		public object TryGetProperty(PropertyDefinition propertyDefinition)
		{
			StorePropertyDefinition propertyDefinition2 = InternalSchema.ToStorePropertyDefinition(propertyDefinition);
			return this.TryGetProperty(propertyDefinition2);
		}

		// Token: 0x06005627 RID: 22055 RVA: 0x00163A28 File Offset: 0x00161C28
		public string ToString(AddressFormat addressFormat)
		{
			if (addressFormat == AddressFormat.Verbose)
			{
				return string.Format("[{0} \"{1}\", {3}:{2}, {4}:{5}]", new object[]
				{
					this.origin,
					this.DisplayName ?? "(none)",
					this.EmailAddress ?? "(none)",
					this.RoutingType ?? "(none)",
					"SMTP",
					this.SmtpEmailAddress ?? "(none)"
				});
			}
			string text = this.routingTypeDriver.FormatAddress(this, addressFormat);
			if (text != null)
			{
				return text;
			}
			throw new NotSupportedException(ServerStrings.EmailFormatNotSupported(addressFormat, this.RoutingType));
		}

		// Token: 0x06005628 RID: 22056 RVA: 0x00163AD3 File Offset: 0x00161CD3
		object[] IReadOnlyPropertyBag.GetProperties(ICollection<PropertyDefinition> propertyDefinitionArray)
		{
			return this.propertyBag.GetProperties<PropertyDefinition>(propertyDefinitionArray);
		}

		// Token: 0x06005629 RID: 22057 RVA: 0x00163AE4 File Offset: 0x00161CE4
		public Participant ChangeOrigin(ParticipantOrigin newOrigin)
		{
			Util.ThrowOnNullArgument(newOrigin, "newOrigin");
			return new Participant.Builder(this)
			{
				Origin = newOrigin
			}.ToParticipant();
		}

		// Token: 0x0600562A RID: 22058 RVA: 0x00163B10 File Offset: 0x00161D10
		public T GetValueOrDefault<T>(StorePropertyDefinition propertyDefinition)
		{
			return this.GetValueOrDefault<T>(propertyDefinition, default(T));
		}

		// Token: 0x0600562B RID: 22059 RVA: 0x00163B30 File Offset: 0x00161D30
		public T GetValueOrDefault<T>(PropertyDefinition propertyDefinition)
		{
			StorePropertyDefinition propertyDefinition2 = InternalSchema.ToStorePropertyDefinition(propertyDefinition);
			return this.GetValueOrDefault<T>(propertyDefinition2, default(T));
		}

		// Token: 0x0600562C RID: 22060 RVA: 0x00163B54 File Offset: 0x00161D54
		public T GetValueOrDefault<T>(StorePropertyDefinition propertyDefinition, T defaultValue)
		{
			return this.propertyBag.GetValueOrDefault<T>(propertyDefinition, defaultValue);
		}

		// Token: 0x0600562D RID: 22061 RVA: 0x00163B63 File Offset: 0x00161D63
		public T? GetValueAsNullable<T>(StorePropertyDefinition propertyDefinition) where T : struct
		{
			return this.propertyBag.GetValueAsNullable<T>(propertyDefinition);
		}

		// Token: 0x0600562E RID: 22062 RVA: 0x00163B74 File Offset: 0x00161D74
		public bool ExistIn(IEnumerable<Participant> participants)
		{
			foreach (Participant participant in participants)
			{
				if (Participant.HasSameEmail(this, participant, true))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600562F RID: 22063 RVA: 0x00163BC8 File Offset: 0x00161DC8
		internal static List<PropValue> ListCoreProperties(string displayName, string emailAddress, string routingType)
		{
			return Participant.ListCoreProperties(displayName, emailAddress, routingType, null);
		}

		// Token: 0x06005630 RID: 22064 RVA: 0x00163BD4 File Offset: 0x00161DD4
		internal static List<PropValue> ListCoreProperties(string displayName, string emailAddress, string routingType, string originalDisplayName)
		{
			List<PropValue> list = new List<PropValue>(4);
			if (displayName != null)
			{
				list.Add(new PropValue(ParticipantSchema.DisplayName, displayName));
			}
			if (emailAddress != null)
			{
				list.Add(new PropValue(ParticipantSchema.EmailAddress, emailAddress));
			}
			if (routingType != null)
			{
				list.Add(new PropValue(ParticipantSchema.RoutingType, routingType));
			}
			if (originalDisplayName != null)
			{
				list.Add(new PropValue(ParticipantSchema.OriginalDisplayName, originalDisplayName));
			}
			return list;
		}

		// Token: 0x06005631 RID: 22065 RVA: 0x00163C39 File Offset: 0x00161E39
		internal static string NormalizeRoutingType(string routingType)
		{
			if (string.IsNullOrEmpty(routingType))
			{
				return null;
			}
			return routingType.ToUpperInvariant();
		}

		// Token: 0x06005632 RID: 22066 RVA: 0x00163C4C File Offset: 0x00161E4C
		internal static bool HasProxyAddress(ADRecipient user, Participant proxy)
		{
			if (!string.IsNullOrEmpty(proxy.EmailAddress))
			{
				IEqualityComparer<string> equalityComparer = proxy.routingTypeDriver.AddressEqualityComparer as IEqualityComparer<string>;
				if (equalityComparer != null)
				{
					if (proxy.RoutingType == "EX" && !string.IsNullOrEmpty(user.LegacyExchangeDN) && equalityComparer.Equals(user.LegacyExchangeDN, proxy.EmailAddress))
					{
						return true;
					}
					foreach (ProxyAddress proxyAddress in user.EmailAddresses)
					{
						ProxyAddressPrefix prefix = ProxyAddressPrefix.GetPrefix(proxy.RoutingType);
						if ((prefix.Equals(proxyAddress.Prefix) || (proxy.RoutingType == "EX" && proxyAddress.Prefix == ProxyAddressPrefix.X500)) && equalityComparer.Equals(proxyAddress.AddressString, proxy.EmailAddress))
						{
							return true;
						}
					}
					return false;
				}
			}
			return false;
		}

		// Token: 0x06005633 RID: 22067 RVA: 0x00163D50 File Offset: 0x00161F50
		public object TryGetProperty(StorePropertyDefinition propertyDefinition)
		{
			return this.propertyBag.TryGetProperty(propertyDefinition);
		}

		// Token: 0x06005634 RID: 22068 RVA: 0x00163D60 File Offset: 0x00161F60
		private static bool InternalHasSameEmail(Participant participant1, Participant participant2, ADObjectId searchRoot, ADSessionSettings sessionSettings, bool canLookup)
		{
			if (object.ReferenceEquals(participant1, participant2))
			{
				return true;
			}
			if (participant1 == null || participant2 == null)
			{
				return false;
			}
			if (participant1.AreAddressesEqual(participant2))
			{
				return true;
			}
			bool result;
			if (Participant.TryMatchParticipantWithADUser(participant1, participant2, out result))
			{
				return result;
			}
			if (Participant.TryMatchParticipantWithADUser(participant2, participant1, out result))
			{
				return result;
			}
			if (participant1.ValidationStatus == ParticipantValidationStatus.NoError && participant2.ValidationStatus == ParticipantValidationStatus.NoError && !StandaloneFuzzing.IsEnabled)
			{
				if (canLookup && participant1.RoutingType == "SMTP")
				{
					participant1 = (Participant.InternalTryConvertTo(new Participant[]
					{
						participant1
					}, searchRoot, sessionSettings, "EX", participant1.GetHashCode())[0] ?? participant1);
				}
				if (canLookup && participant2.RoutingType == "SMTP")
				{
					participant2 = (Participant.InternalTryConvertTo(new Participant[]
					{
						participant2
					}, searchRoot, sessionSettings, "EX", participant2.GetHashCode())[0] ?? participant2);
				}
				return participant1.AreAddressesEqual(participant2);
			}
			return false;
		}

		// Token: 0x06005635 RID: 22069 RVA: 0x00163E54 File Offset: 0x00162054
		private static Participant[] InternalTryConvertTo(Participant[] sources, ADObjectId searchRoot, ADSessionSettings sessionSettings, string destinationRoutingType, int scopingObjectHashCode)
		{
			Participant.Job job = new Participant.Job(sources);
			PropertyDefinition propertyDefinition;
			Participant.BatchBuilder.Execute(job, new Participant.BatchBuilder[]
			{
				Participant.BatchBuilder.ConvertRoutingType(destinationRoutingType, out propertyDefinition),
				Participant.BatchBuilder.RequestAllProperties(),
				Participant.BatchBuilder.CopyPropertiesFromInput(),
				Participant.BatchBuilder.RequestAllProperties(),
				Participant.BatchBuilder.GetPropertiesFromAD(searchRoot, sessionSettings, new PropertyDefinition[]
				{
					propertyDefinition
				})
			});
			Participant[] array = new Participant[sources.Length];
			for (int i = 0; i < sources.Length; i++)
			{
				if (job[i].Result != null)
				{
					array[i] = job[i].Result.ToParticipant();
					array[i].Submitted = job[i].Input.Submitted;
				}
				else
				{
					Participant.BatchBuilderError batchBuilderError = job[i].Error as Participant.BatchBuilderError;
					ExTraceGlobals.StorageTracer.TraceDebug((long)scopingObjectHashCode, "Failed to convert a participant: source=\"{0}\", destination=\"{1}\", error={2}, innerError={3}", new object[]
					{
						sources[i],
						destinationRoutingType,
						job[i].Error,
						(batchBuilderError != null) ? batchBuilderError.InnerError : null
					});
				}
			}
			return array;
		}

		// Token: 0x06005636 RID: 22070 RVA: 0x00163F71 File Offset: 0x00162171
		private static ParticipantOrigin InferOrigin(PropertyBag propertyBag)
		{
			return new OneOffParticipantOrigin();
		}

		// Token: 0x06005637 RID: 22071 RVA: 0x00163F78 File Offset: 0x00162178
		private static ParticipantValidationStatus ValidateStringLength(string value, int maxLength, ParticipantValidationStatus errorStatus)
		{
			if (value == null || value.Length <= maxLength)
			{
				return ParticipantValidationStatus.NoError;
			}
			return errorStatus;
		}

		// Token: 0x06005638 RID: 22072 RVA: 0x00163F8C File Offset: 0x0016218C
		private static ParticipantValidationStatus ValidateAll(params ParticipantValidationStatus[] validationResults)
		{
			ParticipantValidationStatus participantValidationStatus = ParticipantValidationStatus.NoError;
			foreach (ParticipantValidationStatus participantValidationStatus2 in validationResults)
			{
				if (participantValidationStatus2 > participantValidationStatus)
				{
					participantValidationStatus = participantValidationStatus2;
				}
			}
			return participantValidationStatus;
		}

		// Token: 0x06005639 RID: 22073 RVA: 0x00163FB8 File Offset: 0x001621B8
		private static bool TryMatchParticipantWithADUser(Participant participantWithADUser, Participant normalParticipant, out bool hasSameEmail)
		{
			hasSameEmail = false;
			DirectoryParticipantOrigin directoryParticipantOrigin = participantWithADUser.Origin as DirectoryParticipantOrigin;
			if (directoryParticipantOrigin != null)
			{
				ADRecipient adrecipient = directoryParticipantOrigin.ADEntry as ADRecipient;
				if (adrecipient != null)
				{
					hasSameEmail = Participant.HasProxyAddress(adrecipient, normalParticipant);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600563A RID: 22074 RVA: 0x00163FF4 File Offset: 0x001621F4
		private int ComputeHashCode()
		{
			int num = 0;
			foreach (PropertyDefinition propertyDefinition in this.LoadedProperties)
			{
				object obj = this.TryGetProperty(propertyDefinition);
				if (!PropertyError.IsPropertyError(obj) && !(obj is Array))
				{
					num ^= obj.GetHashCode();
				}
			}
			num ^= this.Submitted.GetHashCode();
			return num;
		}

		// Token: 0x0600563B RID: 22075 RVA: 0x00164070 File Offset: 0x00162270
		private string DetectRoutingType(out RoutingTypeDriver detectedRtDriver)
		{
			string result;
			if (RoutingTypeDriver.TryDetectRoutingType(this.propertyBag, out detectedRtDriver, out result))
			{
				return result;
			}
			this.validationStatus.Set(ParticipantValidationStatus.RoutingTypeRequired);
			this.Validate();
			throw new Exception("Participant.Validate() didn't throw on error");
		}

		// Token: 0x0600563C RID: 22076 RVA: 0x001640AC File Offset: 0x001622AC
		private LocalizedString GetValidationMessage()
		{
			ParticipantValidationStatus participantValidationStatus = this.ValidationStatus;
			if (participantValidationStatus <= ParticipantValidationStatus.RoutingTypeRequired)
			{
				if (participantValidationStatus <= ParticipantValidationStatus.AddressAndRoutingTypeMismatch)
				{
					if (participantValidationStatus == ParticipantValidationStatus.NoError)
					{
						return LocalizedString.Empty;
					}
					if (participantValidationStatus == ParticipantValidationStatus.AddressAndRoutingTypeMismatch)
					{
						return ServerStrings.AddressAndRoutingTypeMismatch(this.RoutingType);
					}
				}
				else
				{
					if (participantValidationStatus == ParticipantValidationStatus.AddressRequiredForRoutingType)
					{
						return ServerStrings.AddressRequiredForRoutingType(this.RoutingType);
					}
					if (participantValidationStatus == ParticipantValidationStatus.DisplayNameRequiredForRoutingType)
					{
						return ServerStrings.DisplayNameRequiredForRoutingType(this.RoutingType ?? "(null)");
					}
					if (participantValidationStatus == ParticipantValidationStatus.RoutingTypeRequired)
					{
						return ServerStrings.RoutingTypeRequired;
					}
				}
			}
			else if (participantValidationStatus <= ParticipantValidationStatus.OperationNotSupportedForRoutingType)
			{
				if (participantValidationStatus == ParticipantValidationStatus.InvalidAddressFormat)
				{
					return ServerStrings.InvalidAddressFormat(this.RoutingType, this.EmailAddress);
				}
				if (participantValidationStatus == ParticipantValidationStatus.AddressAndOriginMismatch)
				{
					return ServerStrings.AddressAndOriginMismatch(this.origin);
				}
				if (participantValidationStatus == ParticipantValidationStatus.OperationNotSupportedForRoutingType)
				{
					return LocalizedString.Empty;
				}
			}
			else
			{
				if (participantValidationStatus == ParticipantValidationStatus.DisplayNameTooBig)
				{
					return ServerStrings.ParticipantPropertyTooBig("DisplayName");
				}
				if (participantValidationStatus == ParticipantValidationStatus.EmailAddressTooBig)
				{
					return ServerStrings.ParticipantPropertyTooBig("EmailAddress");
				}
				if (participantValidationStatus == ParticipantValidationStatus.RoutingTypeTooBig)
				{
					return ServerStrings.ParticipantPropertyTooBig("RoutingType");
				}
			}
			return LocalizedString.Empty;
		}

		// Token: 0x0600563D RID: 22077 RVA: 0x001641A4 File Offset: 0x001623A4
		private ParticipantValidationStatus InternalValidate()
		{
			return Participant.ValidateAll(new ParticipantValidationStatus[]
			{
				this.InternalValidateCoreProperties(),
				this.routingTypeDriver.Validate(this),
				this.origin.Validate(this)
			});
		}

		// Token: 0x0600563E RID: 22078 RVA: 0x001641E8 File Offset: 0x001623E8
		private ParticipantValidationStatus InternalValidateCoreProperties()
		{
			return Participant.ValidateAll(new ParticipantValidationStatus[]
			{
				Participant.ValidateStringLength(this.DisplayName, 8000, ParticipantValidationStatus.DisplayNameTooBig),
				Participant.ValidateStringLength(this.EmailAddress, 1860, ParticipantValidationStatus.EmailAddressTooBig),
				Participant.ValidateStringLength(this.RoutingType, 9, ParticipantValidationStatus.RoutingTypeTooBig),
				(this.RoutingType == null || ProxyAddressPrefix.IsPrefixStringValid(this.RoutingType)) ? ParticipantValidationStatus.NoError : ParticipantValidationStatus.InvalidRoutingTypeFormat,
				(this.EmailAddress == null || ProxyAddressBase.IsAddressStringValid(this.EmailAddress)) ? ParticipantValidationStatus.NoError : ParticipantValidationStatus.InvalidAddressFormat
			});
		}

		// Token: 0x0600563F RID: 22079 RVA: 0x00164284 File Offset: 0x00162484
		private void NormalizeCoreProperties()
		{
			string displayName = this.DisplayName;
			if (this.ShouldClearDisplayName(displayName))
			{
				this.propertyBag.Delete(ParticipantSchema.DisplayName);
			}
			foreach (StorePropertyDefinition propertyDefinition in Participant.coreProperties)
			{
				string valueOrDefault = this.GetValueOrDefault<string>(propertyDefinition);
				if (valueOrDefault != null && valueOrDefault.Length == 0)
				{
					this.propertyBag.Delete(propertyDefinition);
				}
			}
		}

		// Token: 0x06005640 RID: 22080 RVA: 0x001642F0 File Offset: 0x001624F0
		private bool ShouldClearDisplayName(string displayName)
		{
			if (displayName == null)
			{
				return false;
			}
			foreach (char c in displayName)
			{
				char c2 = c;
				switch (c2)
				{
				case 'ᅟ':
				case 'ᅠ':
					break;
				default:
					if (c2 != 'ㅤ' && c2 != 'ﾠ' && char.IsLetterOrDigit(c))
					{
						return false;
					}
					break;
				}
			}
			return true;
		}

		// Token: 0x06005641 RID: 22081 RVA: 0x00164358 File Offset: 0x00162558
		private RoutingTypeDriver SelectRoutingTypeDriver()
		{
			string text = this.RoutingType;
			if (text == null)
			{
				RoutingTypeDriver routingTypeDriver = null;
				text = (this.RoutingType = this.DetectRoutingType(out routingTypeDriver));
				if (routingTypeDriver.IsRoutingTypeSupported(text))
				{
					return routingTypeDriver;
				}
			}
			for (int i = 0; i < text.Length; i++)
			{
				if (char.IsLower(text[i]))
				{
					text = (this.RoutingType = Participant.NormalizeRoutingType(text));
					break;
				}
			}
			return RoutingTypeDriver.PickRoutingTypeDriver(text);
		}

		// Token: 0x06005642 RID: 22082 RVA: 0x001643C4 File Offset: 0x001625C4
		public static Participant[] TryConvertTo(Participant[] sources, string destinationRoutingType, IExchangePrincipal scopingPrincipal, ADObjectId searchRoot)
		{
			if (scopingPrincipal == null)
			{
				throw new ArgumentNullException("scopingPrincipal");
			}
			ADSessionSettings sessionSettings = scopingPrincipal.MailboxInfo.OrganizationId.ToADSessionSettings();
			return Participant.InternalTryConvertTo(sources, searchRoot, sessionSettings, destinationRoutingType, scopingPrincipal.GetHashCode());
		}

		// Token: 0x04002E22 RID: 11810
		public const string EX = "EX";

		// Token: 0x04002E23 RID: 11811
		public const string MapiPDL = "MAPIPDL";

		// Token: 0x04002E24 RID: 11812
		public const string SMTP = "SMTP";

		// Token: 0x04002E25 RID: 11813
		public const string FAX = "FAX";

		// Token: 0x04002E26 RID: 11814
		public const string MOBILE = "MOBILE";

		// Token: 0x04002E27 RID: 11815
		public const string Unspecified = null;

		// Token: 0x04002E28 RID: 11816
		internal const int MaxDisplayNameLength = 8000;

		// Token: 0x04002E29 RID: 11817
		internal const int MaxDisplayNamePropertyLenght = 255;

		// Token: 0x04002E2A RID: 11818
		internal const int MaxEmailAddressLength = 1860;

		// Token: 0x04002E2B RID: 11819
		internal const int MaxRoutingTypeLength = 9;

		// Token: 0x04002E2C RID: 11820
		private const string VerboseFormatString = "[{0} \"{1}\", {3}:{2}, {4}:{5}]";

		// Token: 0x04002E2D RID: 11821
		private static readonly char[] DisplayNamesSeparator = new char[]
		{
			';'
		};

		// Token: 0x04002E2E RID: 11822
		private static readonly Participant.TryParseHandler[] parsingChain = new Participant.TryParseHandler[]
		{
			new Participant.TryParseHandler(ExRoutingTypeDriver.TryParseExchangeLegacyDN),
			new Participant.TryParseHandler(GenericCustomRoutingTypeDriver.TryParseOutlookFormat),
			new Participant.TryParseHandler(MobileRoutingTypeDriver.TryParseMobilePhoneNumber),
			new Participant.TryParseHandler(UnspecifiedRoutingTypeDriver.TryParse)
		};

		// Token: 0x04002E2F RID: 11823
		private static readonly StorePropertyDefinition[] coreProperties = new StorePropertyDefinition[]
		{
			ParticipantSchema.DisplayName,
			ParticipantSchema.EmailAddress,
			ParticipantSchema.RoutingType,
			ParticipantSchema.OriginalDisplayName
		};

		// Token: 0x04002E30 RID: 11824
		private readonly LazilyInitialized<int> hashCode;

		// Token: 0x04002E31 RID: 11825
		private readonly ParticipantOrigin origin;

		// Token: 0x04002E32 RID: 11826
		private readonly Participant.ParticipantPropertyBag propertyBag;

		// Token: 0x04002E33 RID: 11827
		private readonly RoutingTypeDriver routingTypeDriver;

		// Token: 0x04002E34 RID: 11828
		private readonly LazilyInitialized<ParticipantValidationStatus> validationStatus;

		// Token: 0x020008F5 RID: 2293
		// (Invoke) Token: 0x06005645 RID: 22085
		private delegate IEnumerable<PropValue> TryParseHandler(string inputString);

		// Token: 0x020008F6 RID: 2294
		private sealed class ParticipantPropertyBag : MemoryPropertyBag
		{
			// Token: 0x06005648 RID: 22088 RVA: 0x00164495 File Offset: 0x00162695
			public ParticipantPropertyBag(Participant.ParticipantPropertyBag copyFrom) : base(copyFrom)
			{
			}

			// Token: 0x06005649 RID: 22089 RVA: 0x001644A0 File Offset: 0x001626A0
			public ParticipantPropertyBag(IEnumerable<PropValue> propValues)
			{
				foreach (PropValue propValue in propValues)
				{
					if (base.ContainsKey(propValue.Property))
					{
						throw new ArgumentException();
					}
					base[propValue.Property] = propValue.Value;
				}
				base.SetAllPropertiesLoaded();
			}

			// Token: 0x0600564A RID: 22090 RVA: 0x00164518 File Offset: 0x00162718
			public void Freeze()
			{
				if (!this.isFrozen)
				{
					this.isFrozen = true;
					base.ClearChangeInfo();
				}
			}

			// Token: 0x0600564B RID: 22091 RVA: 0x00164530 File Offset: 0x00162730
			public void SetProperties(IEnumerable<PropValue> properties)
			{
				foreach (PropValue propValue in properties)
				{
					base[propValue.Property] = propValue.Value;
				}
			}

			// Token: 0x0600564C RID: 22092 RVA: 0x00164588 File Offset: 0x00162788
			public void SetDefaultProperties(IEnumerable<PropValue> properties)
			{
				foreach (PropValue propValue in properties)
				{
					if (PropertyError.IsPropertyNotFound(base.TryGetProperty(propValue.Property)))
					{
						base[propValue.Property] = propValue.Value;
					}
				}
			}

			// Token: 0x0600564D RID: 22093 RVA: 0x001645F4 File Offset: 0x001627F4
			protected override void SetValidatedStoreProperty(StorePropertyDefinition propertyDefinition, object propertyValue)
			{
				this.CheckCanModify(propertyDefinition);
				base.SetValidatedStoreProperty(propertyDefinition, propertyValue);
			}

			// Token: 0x0600564E RID: 22094 RVA: 0x00164605 File Offset: 0x00162805
			protected override void DeleteStoreProperty(StorePropertyDefinition propertyDefinition)
			{
				this.CheckCanModify(propertyDefinition);
				base.DeleteStoreProperty(propertyDefinition);
			}

			// Token: 0x0600564F RID: 22095 RVA: 0x00164618 File Offset: 0x00162818
			private void CheckCanModify(PropertyDefinition propertyDefinition)
			{
				if (this.isFrozen)
				{
					throw PropertyError.ToException(new PropertyError[]
					{
						new PropertyError(propertyDefinition, PropertyErrorCode.NotSupported)
					});
				}
			}

			// Token: 0x04002E36 RID: 11830
			private bool isFrozen;
		}

		// Token: 0x020008F7 RID: 2295
		public sealed class Builder
		{
			// Token: 0x06005650 RID: 22096 RVA: 0x00164645 File Offset: 0x00162845
			public Builder()
			{
				this.propertyBag = new Participant.ParticipantPropertyBag(Array<PropValue>.Empty);
			}

			// Token: 0x06005651 RID: 22097 RVA: 0x0016465D File Offset: 0x0016285D
			public Builder(Participant copyFrom)
			{
				this.propertyBag = new Participant.ParticipantPropertyBag(copyFrom.propertyBag);
				this.origin = copyFrom.origin;
			}

			// Token: 0x06005652 RID: 22098 RVA: 0x00164682 File Offset: 0x00162882
			public Builder(string displayName, string emailAddress, string routingType) : this()
			{
				this.DisplayName = displayName;
				this.EmailAddress = emailAddress;
				this.RoutingType = routingType;
			}

			// Token: 0x17001825 RID: 6181
			public object this[StorePropertyDefinition propDef]
			{
				get
				{
					this.CheckState();
					return this.propertyBag[propDef];
				}
				set
				{
					this.CheckState();
					this.propertyBag[propDef] = value;
				}
			}

			// Token: 0x17001826 RID: 6182
			// (get) Token: 0x06005655 RID: 22101 RVA: 0x001646C8 File Offset: 0x001628C8
			// (set) Token: 0x06005656 RID: 22102 RVA: 0x001646E0 File Offset: 0x001628E0
			public string DisplayName
			{
				get
				{
					this.CheckState();
					return this.propertyBag.GetValueOrDefault<string>(ParticipantSchema.DisplayName);
				}
				set
				{
					this.CheckState();
					this.propertyBag.SetOrDeleteProperty(ParticipantSchema.DisplayName, value);
				}
			}

			// Token: 0x17001827 RID: 6183
			// (get) Token: 0x06005657 RID: 22103 RVA: 0x001646F9 File Offset: 0x001628F9
			// (set) Token: 0x06005658 RID: 22104 RVA: 0x00164711 File Offset: 0x00162911
			public string EmailAddress
			{
				get
				{
					this.CheckState();
					return this.propertyBag.GetValueOrDefault<string>(ParticipantSchema.EmailAddress);
				}
				set
				{
					this.CheckState();
					this.propertyBag.SetOrDeleteProperty(ParticipantSchema.EmailAddress, value);
				}
			}

			// Token: 0x17001828 RID: 6184
			// (get) Token: 0x06005659 RID: 22105 RVA: 0x0016472A File Offset: 0x0016292A
			// (set) Token: 0x0600565A RID: 22106 RVA: 0x00164738 File Offset: 0x00162938
			public ParticipantOrigin Origin
			{
				get
				{
					this.CheckState();
					return this.origin;
				}
				set
				{
					this.CheckState();
					this.origin = value;
				}
			}

			// Token: 0x17001829 RID: 6185
			// (get) Token: 0x0600565B RID: 22107 RVA: 0x00164747 File Offset: 0x00162947
			// (set) Token: 0x0600565C RID: 22108 RVA: 0x0016475F File Offset: 0x0016295F
			public string RoutingType
			{
				get
				{
					this.CheckState();
					return this.propertyBag.GetValueOrDefault<string>(ParticipantSchema.RoutingType);
				}
				set
				{
					this.CheckState();
					this.propertyBag.SetOrDeleteProperty(ParticipantSchema.RoutingType, value);
				}
			}

			// Token: 0x0600565D RID: 22109 RVA: 0x00164778 File Offset: 0x00162978
			public void SetPropertiesFrom(ParticipantEntryId entryId)
			{
				if (entryId == null)
				{
					throw new ArgumentNullException("entryId");
				}
				this.CheckState();
				this.Add(entryId.GetParticipantProperties());
				this.origin = (entryId.GetParticipantOrigin() ?? this.origin);
			}

			// Token: 0x0600565E RID: 22110 RVA: 0x001647B0 File Offset: 0x001629B0
			public Participant ToParticipant()
			{
				this.CheckState();
				Participant result = new Participant(this.origin, this.propertyBag);
				this.Invalidate();
				return result;
			}

			// Token: 0x0600565F RID: 22111 RVA: 0x001647DC File Offset: 0x001629DC
			public object TryGetProperty(PropertyDefinition propDef)
			{
				return this.propertyBag.TryGetProperty(propDef);
			}

			// Token: 0x06005660 RID: 22112 RVA: 0x001647EA File Offset: 0x001629EA
			internal void Add(IEnumerable<PropValue> propValues)
			{
				this.CheckState();
				this.propertyBag.SetProperties(propValues);
			}

			// Token: 0x06005661 RID: 22113 RVA: 0x00164800 File Offset: 0x00162A00
			internal T GetValueOrDefault<T>(StorePropertyDefinition propertyDefinition)
			{
				return this.GetValueOrDefault<T>(propertyDefinition, default(T));
			}

			// Token: 0x06005662 RID: 22114 RVA: 0x0016481D File Offset: 0x00162A1D
			internal T GetValueOrDefault<T>(StorePropertyDefinition propertyDefinition, T defaultValue)
			{
				return PropertyBag.CheckPropertyValue<T>(propertyDefinition, this.TryGetProperty(propertyDefinition), defaultValue);
			}

			// Token: 0x06005663 RID: 22115 RVA: 0x0016482D File Offset: 0x00162A2D
			internal void SetOrDeleteProperty(StorePropertyDefinition propertyDefinition, object propertyValue)
			{
				this.propertyBag.SetOrDeleteProperty(propertyDefinition, propertyValue);
			}

			// Token: 0x06005664 RID: 22116 RVA: 0x0016483C File Offset: 0x00162A3C
			private void CheckState()
			{
				if (this.propertyBag == null)
				{
					throw new InvalidOperationException("For performance reasons, Participant.Builder instance cannot be reused once ToParticipant() has been called on it");
				}
			}

			// Token: 0x06005665 RID: 22117 RVA: 0x00164851 File Offset: 0x00162A51
			private void Invalidate()
			{
				this.propertyBag = null;
				this.origin = null;
			}

			// Token: 0x04002E37 RID: 11831
			private ParticipantOrigin origin;

			// Token: 0x04002E38 RID: 11832
			private Participant.ParticipantPropertyBag propertyBag;
		}

		// Token: 0x020008F8 RID: 2296
		internal abstract class BatchBuilder
		{
			// Token: 0x1400000B RID: 11
			// (add) Token: 0x06005667 RID: 22119 RVA: 0x0016486C File Offset: 0x00162A6C
			// (remove) Token: 0x06005668 RID: 22120 RVA: 0x001648A4 File Offset: 0x00162AA4
			private event Action<Participant.JobItem> ErrorSet;

			// Token: 0x06005669 RID: 22121 RVA: 0x001648D9 File Offset: 0x00162AD9
			public static Participant.BatchBuilder ImceaEncode(string incapsulationDomain)
			{
				return new Participant.ImceaEncoderBatchBuilder(incapsulationDomain, null);
			}

			// Token: 0x0600566A RID: 22122 RVA: 0x001648E4 File Offset: 0x00162AE4
			public static Participant.BatchBuilder ConvertRoutingType(string destinationRoutingType, out PropertyDefinition keyProperty)
			{
				Participant.RoutingTypeConverterBatchBuider routingTypeConverterBatchBuider = new Participant.RoutingTypeConverterBatchBuider(destinationRoutingType, null);
				keyProperty = routingTypeConverterBatchBuider.SpecializedAddressPropertyDefinition;
				return new Participant.RoutingTypeConverterBatchBuider(destinationRoutingType, null);
			}

			// Token: 0x0600566B RID: 22123 RVA: 0x00164908 File Offset: 0x00162B08
			public static Participant.BatchBuilder CopyPropertiesFromInput()
			{
				return new Participant.CopyFromInputBatchBuilder();
			}

			// Token: 0x0600566C RID: 22124 RVA: 0x0016490F File Offset: 0x00162B0F
			public static Participant.BatchBuilder ReplaceProperty(StorePropertyDefinition propertyDefinitionToReplace, StorePropertyDefinition propertyDefinitionToReplaceWith)
			{
				return new Participant.ReplacePropertyBatchBuilder(propertyDefinitionToReplace, propertyDefinitionToReplaceWith);
			}

			// Token: 0x0600566D RID: 22125 RVA: 0x00164918 File Offset: 0x00162B18
			public static Participant.BatchBuilder RequestProperties(ICollection<PropertyDefinition> propDefs)
			{
				return new Participant.RequestPropertiesBatchBuilder(propDefs);
			}

			// Token: 0x0600566E RID: 22126 RVA: 0x00164920 File Offset: 0x00162B20
			public static Participant.BatchBuilder RequestAllProperties()
			{
				return new Participant.RequestPropertiesBatchBuilder(ParticipantSchema.Instance.InternalAllProperties);
			}

			// Token: 0x0600566F RID: 22127 RVA: 0x00164931 File Offset: 0x00162B31
			public static Participant.BatchBuilder RequestProperties(params PropertyDefinition[] propDefs)
			{
				return new Participant.RequestPropertiesBatchBuilder((ICollection<PropertyDefinition>)propDefs);
			}

			// Token: 0x06005670 RID: 22128 RVA: 0x00164940 File Offset: 0x00162B40
			public static void Execute(Participant.Job job, params Participant.BatchBuilder[] batchBuilders)
			{
				LinkedListNode<Participant.BatchBuilder> linkedListNode = Participant.BatchBuilder.CreateChain(batchBuilders);
				linkedListNode.Value.Execute(job, linkedListNode.Next);
			}

			// Token: 0x06005671 RID: 22129 RVA: 0x0016496E File Offset: 0x00162B6E
			public Participant.BatchBuilder SuppressErrors()
			{
				this.ErrorSet += delegate(Participant.JobItem jobItem)
				{
					jobItem.IgnoreError();
				};
				return this;
			}

			// Token: 0x06005672 RID: 22130 RVA: 0x001649B4 File Offset: 0x00162BB4
			public Participant.BatchBuilder HandleErrors(Predicate<Participant.JobItem> errorHandler)
			{
				this.ErrorSet += delegate(Participant.JobItem jobItem)
				{
					if (errorHandler(jobItem))
					{
						jobItem.IgnoreError();
					}
				};
				return this;
			}

			// Token: 0x06005673 RID: 22131 RVA: 0x001649E1 File Offset: 0x00162BE1
			internal static ADSessionSettings GetADSessionSettings(ICoreObject storeObject)
			{
				if (storeObject != null)
				{
					return Participant.BatchBuilder.GetADSessionSettings(storeObject.Session);
				}
				return Participant.BatchBuilder.GetADSessionSettings(null);
			}

			// Token: 0x06005674 RID: 22132 RVA: 0x001649F8 File Offset: 0x00162BF8
			internal static ADSessionSettings GetADSessionSettings(StoreSession session)
			{
				if (session != null)
				{
					return session.GetADSessionSettings();
				}
				return OrganizationId.ForestWideOrgId.ToADSessionSettings();
			}

			// Token: 0x06005675 RID: 22133 RVA: 0x00164A0E File Offset: 0x00162C0E
			protected internal virtual void Execute(Participant.Job job, LinkedListNode<Participant.BatchBuilder> nextBatchBuilder)
			{
				if (nextBatchBuilder != null)
				{
					nextBatchBuilder.Value.Execute(job, nextBatchBuilder.Next);
				}
			}

			// Token: 0x06005676 RID: 22134 RVA: 0x00164A25 File Offset: 0x00162C25
			protected virtual void AddToChain(LinkedList<Participant.BatchBuilder> chain)
			{
				chain.AddLast(this);
			}

			// Token: 0x06005677 RID: 22135 RVA: 0x00164A2F File Offset: 0x00162C2F
			protected void SetError(Participant.JobItem jobItem, string descritpion, ProviderError innerError)
			{
				jobItem.InternalSetError(new Participant.BatchBuilderError(this, descritpion, innerError));
				if (this.ErrorSet != null)
				{
					this.ErrorSet(jobItem);
				}
			}

			// Token: 0x06005678 RID: 22136 RVA: 0x00164A54 File Offset: 0x00162C54
			private static LinkedListNode<Participant.BatchBuilder> CreateChain(Participant.BatchBuilder[] batchBuilders)
			{
				LinkedList<Participant.BatchBuilder> linkedList = new LinkedList<Participant.BatchBuilder>();
				foreach (Participant.BatchBuilder batchBuilder in batchBuilders)
				{
					if (batchBuilder != null)
					{
						batchBuilder.AddToChain(linkedList);
					}
				}
				if (linkedList.First == null)
				{
					throw new ArgumentException();
				}
				return linkedList.First;
			}

			// Token: 0x06005679 RID: 22137 RVA: 0x00164A99 File Offset: 0x00162C99
			public static Participant.BatchBuilder GetPropertiesFromAD(IADRecipientCache recipientCache, params PropertyDefinition[] keyProperties)
			{
				return new Participant.ADLookupBatchBuilder(new Participant.ADLookupBatchBuilder.LookupADRecipientDelegate(recipientCache.FindAndCacheRecipients), keyProperties);
			}

			// Token: 0x0600567A RID: 22138 RVA: 0x00164AD0 File Offset: 0x00162CD0
			public static Participant.BatchBuilder GetPropertiesFromAD(IRecipientSession recipientSession, params PropertyDefinition[] keyProperties)
			{
				return new Participant.ADLookupBatchBuilder((ProxyAddress[] proxyAddresses) => recipientSession.FindByProxyAddresses(proxyAddresses, Util.CollectionToArray<ADPropertyDefinition>(ParticipantSchema.SupportedADProperties)), keyProperties);
			}

			// Token: 0x0600567B RID: 22139 RVA: 0x00164AFC File Offset: 0x00162CFC
			public static Participant.BatchBuilder GetPropertiesFromAD(ADObjectId searchRoot, ADSessionSettings adSettings, params PropertyDefinition[] keyProperties)
			{
				Util.ThrowOnNullArgument(adSettings, "adSettings");
				if (adSettings.ConfigScopes == ConfigScopes.TenantLocal && OrganizationId.ForestWideOrgId.Equals(adSettings.CurrentOrganizationId))
				{
					adSettings = ADSessionSettings.RescopeToSubtree(adSettings);
				}
				return Participant.BatchBuilder.GetPropertiesFromAD(DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, searchRoot, CultureInfo.CurrentCulture.LCID, true, ConsistencyMode.PartiallyConsistent, null, adSettings, 817, "GetPropertiesFromAD", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\Items\\Participants\\ParticipantBatchBuilder.cs"), keyProperties);
			}
		}

		// Token: 0x020008F9 RID: 2297
		private class ImceaEncoderBatchBuilder : Participant.BatchBuilder
		{
			// Token: 0x0600567D RID: 22141 RVA: 0x00164B66 File Offset: 0x00162D66
			internal ImceaEncoderBatchBuilder(string incapsulationDomain, Predicate<Participant.JobItem> isEncodingNeeded)
			{
				this.incapsulationDomain = incapsulationDomain;
				this.isEncodingNeeded = isEncodingNeeded;
			}

			// Token: 0x0600567E RID: 22142 RVA: 0x00164B7C File Offset: 0x00162D7C
			protected internal override void Execute(Participant.Job job, LinkedListNode<Participant.BatchBuilder> nextBatchBuilder)
			{
				base.Execute(job, nextBatchBuilder);
				foreach (Participant.JobItem jobItem in job.GetActiveJobItems())
				{
					if (this.IsEncodingNeeded(jobItem))
					{
						string valueOrDefault = jobItem.GetValueOrDefault<string>(ParticipantSchema.EmailAddress);
						string valueOrDefault2 = jobItem.GetValueOrDefault<string>(ParticipantSchema.RoutingType);
						if (!string.IsNullOrEmpty(valueOrDefault) && !string.IsNullOrEmpty(valueOrDefault2))
						{
							jobItem.SetOrDeleteProperty(ParticipantSchema.EmailAddress, ImceaAddress.Encode(valueOrDefault2, valueOrDefault, this.incapsulationDomain));
							jobItem.SetOrDeleteProperty(ParticipantSchema.RoutingType, "SMTP");
						}
						else
						{
							base.SetError(jobItem, ServerStrings.ExBatchBuilderNeedsPropertyToConvertRT(ParticipantSchema.EmailAddress, valueOrDefault2, "SMTP", jobItem.Input.ToString()), null);
						}
					}
				}
			}

			// Token: 0x0600567F RID: 22143 RVA: 0x00164C54 File Offset: 0x00162E54
			protected virtual bool IsEncodingNeeded(Participant.JobItem jobItem)
			{
				return !Participant.RoutingTypeEquals(jobItem.GetValueOrDefault<string>(ParticipantSchema.RoutingType), "SMTP") && (this.isEncodingNeeded == null || this.isEncodingNeeded(jobItem));
			}

			// Token: 0x04002E3B RID: 11835
			private readonly string incapsulationDomain;

			// Token: 0x04002E3C RID: 11836
			private readonly Predicate<Participant.JobItem> isEncodingNeeded;
		}

		// Token: 0x020008FA RID: 2298
		private class RoutingTypeConverterBatchBuider : Participant.BatchBuilder
		{
			// Token: 0x06005680 RID: 22144 RVA: 0x00164C85 File Offset: 0x00162E85
			internal RoutingTypeConverterBatchBuider(string destinationRoutingType, Predicate<Participant.JobItem> isConversionNeeded)
			{
				this.destinationRoutingType = Participant.NormalizeRoutingType(destinationRoutingType);
				this.isConversionNeeded = isConversionNeeded;
				this.specializedAddressPropDef = Participant.RoutingTypeConverterBatchBuider.GetSpecializedAddressPropertyForRoutingType(destinationRoutingType);
			}

			// Token: 0x1700182A RID: 6186
			// (get) Token: 0x06005681 RID: 22145 RVA: 0x00164CAC File Offset: 0x00162EAC
			internal StorePropertyDefinition SpecializedAddressPropertyDefinition
			{
				get
				{
					return this.specializedAddressPropDef;
				}
			}

			// Token: 0x06005682 RID: 22146 RVA: 0x00164CB4 File Offset: 0x00162EB4
			protected internal override void Execute(Participant.Job job, LinkedListNode<Participant.BatchBuilder> nextBatchBuilder)
			{
				foreach (Participant.JobItem jobItem in job.GetActiveJobItems())
				{
					if (this.IsConversionNeeded(jobItem))
					{
						jobItem.RequestProperty(this.specializedAddressPropDef);
					}
				}
				base.Execute(job, nextBatchBuilder);
				foreach (Participant.JobItem jobItem2 in job.GetActiveJobItems())
				{
					if (this.IsConversionNeeded(jobItem2))
					{
						string valueOrDefault = jobItem2.Result.GetValueOrDefault<string>(this.specializedAddressPropDef);
						if (valueOrDefault != null)
						{
							jobItem2.SetOrDeleteProperty(ParticipantSchema.EmailAddress, valueOrDefault);
							jobItem2.SetOrDeleteProperty(ParticipantSchema.RoutingType, this.destinationRoutingType);
						}
						else
						{
							base.SetError(jobItem2, ServerStrings.ExBatchBuilderNeedsPropertyToConvertRT(this.specializedAddressPropDef, jobItem2.Input.RoutingType, this.destinationRoutingType, jobItem2.Input.ToString()), null);
						}
					}
				}
			}

			// Token: 0x06005683 RID: 22147 RVA: 0x00164DC8 File Offset: 0x00162FC8
			protected virtual bool IsConversionNeeded(Participant.JobItem jobItem)
			{
				string valueOrDefault = jobItem.GetValueOrDefault<string>(ParticipantSchema.RoutingType);
				return !Participant.RoutingTypeEquals(valueOrDefault, this.destinationRoutingType) && (this.isConversionNeeded == null || this.isConversionNeeded(jobItem));
			}

			// Token: 0x06005684 RID: 22148 RVA: 0x00164E08 File Offset: 0x00163008
			private static StorePropertyDefinition GetSpecializedAddressPropertyForRoutingType(string routingType)
			{
				if (routingType != null)
				{
					if (routingType == "SMTP")
					{
						return ParticipantSchema.SmtpAddress;
					}
					if (routingType == "EX")
					{
						return ParticipantSchema.LegacyExchangeDN;
					}
				}
				throw new NotSupportedException(string.Format("Batch conversion to {0} routing type is not supported", routingType));
			}

			// Token: 0x04002E3D RID: 11837
			private readonly string destinationRoutingType;

			// Token: 0x04002E3E RID: 11838
			private readonly Predicate<Participant.JobItem> isConversionNeeded;

			// Token: 0x04002E3F RID: 11839
			private readonly StorePropertyDefinition specializedAddressPropDef;
		}

		// Token: 0x020008FB RID: 2299
		private sealed class RequestPropertiesBatchBuilder : Participant.BatchBuilder
		{
			// Token: 0x06005685 RID: 22149 RVA: 0x00164E52 File Offset: 0x00163052
			internal RequestPropertiesBatchBuilder(ICollection<PropertyDefinition> propDefs)
			{
				this.propDefs = InternalSchema.ToStorePropertyDefinitions(propDefs);
			}

			// Token: 0x06005686 RID: 22150 RVA: 0x00164E68 File Offset: 0x00163068
			protected internal override void Execute(Participant.Job job, LinkedListNode<Participant.BatchBuilder> nextBatchBuilder)
			{
				foreach (Participant.JobItem jobItem in job.GetActiveJobItems())
				{
					jobItem.RequestProperties(this.propDefs);
				}
				base.Execute(job, nextBatchBuilder);
			}

			// Token: 0x04002E40 RID: 11840
			private readonly StorePropertyDefinition[] propDefs;
		}

		// Token: 0x020008FC RID: 2300
		private sealed class CopyFromInputBatchBuilder : Participant.BatchBuilder
		{
			// Token: 0x06005687 RID: 22151 RVA: 0x00164EC4 File Offset: 0x001630C4
			internal CopyFromInputBatchBuilder()
			{
			}

			// Token: 0x06005688 RID: 22152 RVA: 0x00164ECC File Offset: 0x001630CC
			protected internal override void Execute(Participant.Job job, LinkedListNode<Participant.BatchBuilder> nextBatchBuilder)
			{
				foreach (Participant.JobItem jobItem in job.GetActiveJobItems())
				{
					if (jobItem.Result.Origin == null)
					{
						jobItem.Result.Origin = jobItem.Input.Origin;
					}
					foreach (StorePropertyDefinition storePropertyDefinition in new List<StorePropertyDefinition>(jobItem.RequestedProperties))
					{
						if ((storePropertyDefinition.PropertyFlags & PropertyFlags.ReadOnly) == PropertyFlags.None)
						{
							jobItem.SetOrDeleteProperty(storePropertyDefinition, jobItem.Input.TryGetProperty(storePropertyDefinition));
						}
					}
				}
				base.Execute(job, nextBatchBuilder);
			}
		}

		// Token: 0x020008FD RID: 2301
		private sealed class ReplacePropertyBatchBuilder : Participant.BatchBuilder
		{
			// Token: 0x06005689 RID: 22153 RVA: 0x00164FA0 File Offset: 0x001631A0
			internal ReplacePropertyBatchBuilder(StorePropertyDefinition propertyDefinitionToReplace, StorePropertyDefinition propertyDefinitionToReplaceWith)
			{
				this.propertyDefinitionToReplace = propertyDefinitionToReplace;
				this.propertyDefinitionToReplaceWith = propertyDefinitionToReplaceWith;
			}

			// Token: 0x0600568A RID: 22154 RVA: 0x00164FB8 File Offset: 0x001631B8
			protected internal override void Execute(Participant.Job job, LinkedListNode<Participant.BatchBuilder> nextBatchBuilder)
			{
				base.Execute(job, nextBatchBuilder);
				foreach (Participant.JobItem jobItem in job.GetActiveJobItems())
				{
					jobItem.SetOrDeleteProperty(this.propertyDefinitionToReplace, jobItem.Result.TryGetProperty(this.propertyDefinitionToReplaceWith));
				}
			}

			// Token: 0x04002E41 RID: 11841
			private readonly StorePropertyDefinition propertyDefinitionToReplace;

			// Token: 0x04002E42 RID: 11842
			private readonly StorePropertyDefinition propertyDefinitionToReplaceWith;
		}

		// Token: 0x020008FE RID: 2302
		private class ADLookupBatchBuilder : Participant.BatchBuilder
		{
			// Token: 0x0600568B RID: 22155 RVA: 0x00165024 File Offset: 0x00163224
			internal ADLookupBatchBuilder(Participant.ADLookupBatchBuilder.LookupADRecipientDelegate lookupADRecipientDelegate, IList<PropertyDefinition> keyProperties)
			{
				if (lookupADRecipientDelegate == null)
				{
					throw new ArgumentNullException("lookupADRecipientDelegate");
				}
				if (keyProperties != null && keyProperties.Count < 1)
				{
					throw new ArgumentOutOfRangeException("keyProperties.Length");
				}
				this.lookupADRecipientDelegate = (StandaloneFuzzing.IsEnabled ? new Participant.ADLookupBatchBuilder.LookupADRecipientDelegate(Participant.ADLookupBatchBuilder.NoADLookup) : lookupADRecipientDelegate);
				if (keyProperties != null)
				{
					this.keyProperties = new HashSet<StorePropertyDefinition>(InternalSchema.ToStorePropertyDefinitions(keyProperties));
				}
			}

			// Token: 0x0600568C RID: 22156 RVA: 0x0016508C File Offset: 0x0016328C
			protected internal override void Execute(Participant.Job job, LinkedListNode<Participant.BatchBuilder> nextBatchBuilder)
			{
				List<Participant.JobItem> list;
				List<ProxyAddress> list2;
				this.SelectJobItemsToLookup(job, out list, out list2);
				IList<Result<ADRawEntry>> list3;
				try
				{
					list3 = this.lookupADRecipientDelegate(list2.ToArray());
				}
				catch (DataSourceOperationException ex)
				{
					throw StorageGlobals.TranslateDirectoryException(ServerStrings.ADException, ex, null, "Participant.ADRecipientCacheBatchBuilder.Execute. Failed due to directory exception {0}.", new object[]
					{
						ex
					});
				}
				catch (DataSourceTransientException ex2)
				{
					throw StorageGlobals.TranslateDirectoryException(ServerStrings.ADException, ex2, null, "Participant.ADRecipientCacheBatchBuilder.Execute. Failed due to directory exception {0}.", new object[]
					{
						ex2
					});
				}
				for (int i = 0; i < list3.Count; i++)
				{
					if (list3[i].Data != null)
					{
						Participant.ADLookupBatchBuilder.CopyPropertiesFromAD(list[i], list3[i].Data, new List<StorePropertyDefinition>(list[i].RequestedProperties));
					}
					if (list3[i].Error != null)
					{
						base.SetError(list[i], ServerStrings.ExBatchBuilderADLookupFailed(list2[i], list3[i].Error), list3[i].Error);
					}
				}
				base.Execute(job, nextBatchBuilder);
			}

			// Token: 0x0600568D RID: 22157 RVA: 0x001651D8 File Offset: 0x001633D8
			private static IList<Result<ADRawEntry>> NoADLookup(IList<ProxyAddress> proxyAddresses)
			{
				return Array<Result<ADRawEntry>>.Empty;
			}

			// Token: 0x0600568E RID: 22158 RVA: 0x001651E0 File Offset: 0x001633E0
			private static void CopyPropertiesFromAD(Participant.JobItem jobItem, ADRawEntry source, IEnumerable<StorePropertyDefinition> propertyDefinitions)
			{
				ConversionPropertyBag conversionPropertyBag = new ConversionPropertyBag(source, StoreToDirectorySchemaConverter.Instance);
				foreach (StorePropertyDefinition storePropertyDefinition in propertyDefinitions)
				{
					if ((storePropertyDefinition.PropertyFlags & PropertyFlags.ReadOnly) == PropertyFlags.None)
					{
						object propertyValue = conversionPropertyBag.TryGetProperty(storePropertyDefinition);
						if (!PropertyError.IsPropertyError(propertyValue))
						{
							jobItem.SetOrDeleteProperty(storePropertyDefinition, propertyValue);
						}
					}
				}
				if (jobItem.Result.Origin == null || jobItem.Result.Origin is OneOffParticipantOrigin || jobItem.Result.Origin is DirectoryParticipantOrigin)
				{
					jobItem.Result.Origin = new DirectoryParticipantOrigin(source);
				}
			}

			// Token: 0x0600568F RID: 22159 RVA: 0x00165294 File Offset: 0x00163494
			private void SelectJobItemsToLookup(Participant.Job job, out List<Participant.JobItem> jobItemsToLookup, out List<ProxyAddress> proxyAddresses)
			{
				jobItemsToLookup = new List<Participant.JobItem>(job.Count);
				proxyAddresses = new List<ProxyAddress>(jobItemsToLookup.Capacity);
				foreach (Participant.JobItem jobItem in job.GetActiveJobItems())
				{
					Participant participant = new Participant(null, jobItem.GetValueOrDefault<string>(ParticipantSchema.EmailAddress), jobItem.GetValueOrDefault<string>(ParticipantSchema.RoutingType));
					if (participant.EmailAddress != null)
					{
						if (participant.ValidationStatus == ParticipantValidationStatus.NoError)
						{
							if ((this.keyProperties == null && jobItem.AnyPropertiesRequested) || jobItem.AreSomePropertiesRequested(this.keyProperties))
							{
								jobItemsToLookup.Add(jobItem);
								proxyAddresses.Add(new CustomProxyAddress(new CustomProxyAddressPrefix(participant.RoutingType), participant.EmailAddress, false));
							}
						}
						else
						{
							base.SetError(jobItem, participant.GetValidationMessage(), null);
						}
					}
				}
			}

			// Token: 0x04002E43 RID: 11843
			private readonly HashSet<StorePropertyDefinition> keyProperties;

			// Token: 0x04002E44 RID: 11844
			private readonly Participant.ADLookupBatchBuilder.LookupADRecipientDelegate lookupADRecipientDelegate;

			// Token: 0x020008FF RID: 2303
			// (Invoke) Token: 0x06005691 RID: 22161
			internal delegate IList<Result<ADRawEntry>> LookupADRecipientDelegate(ProxyAddress[] proxyAddresses);
		}

		// Token: 0x02000900 RID: 2304
		internal class JobItem
		{
			// Token: 0x06005694 RID: 22164 RVA: 0x00165384 File Offset: 0x00163584
			public JobItem(Participant input)
			{
				this.input = input;
			}

			// Token: 0x06005695 RID: 22165 RVA: 0x001653A9 File Offset: 0x001635A9
			public JobItem(Participant input, Action<Result<Participant>> resultSetter) : this(input)
			{
				this.resultSetter = resultSetter;
			}

			// Token: 0x1700182B RID: 6187
			// (get) Token: 0x06005696 RID: 22166 RVA: 0x001653B9 File Offset: 0x001635B9
			public ProviderError Error
			{
				get
				{
					return this.error;
				}
			}

			// Token: 0x1700182C RID: 6188
			// (get) Token: 0x06005697 RID: 22167 RVA: 0x001653C1 File Offset: 0x001635C1
			public Participant Input
			{
				get
				{
					return this.input;
				}
			}

			// Token: 0x1700182D RID: 6189
			// (get) Token: 0x06005698 RID: 22168 RVA: 0x001653C9 File Offset: 0x001635C9
			public Participant.Builder Result
			{
				get
				{
					if (!this.IsActive)
					{
						return null;
					}
					return this.result;
				}
			}

			// Token: 0x1700182E RID: 6190
			// (get) Token: 0x06005699 RID: 22169 RVA: 0x001653DC File Offset: 0x001635DC
			internal bool AnyPropertiesRequested
			{
				get
				{
					bool flag;
					using (IEnumerator<StorePropertyDefinition> enumerator = this.RequestedProperties.GetEnumerator())
					{
						flag = enumerator.MoveNext();
					}
					return flag;
				}
			}

			// Token: 0x1700182F RID: 6191
			// (get) Token: 0x0600569A RID: 22170 RVA: 0x0016541C File Offset: 0x0016361C
			internal bool IsActive
			{
				get
				{
					return this.input != null && !this.inactiveBecauseOfError;
				}
			}

			// Token: 0x17001830 RID: 6192
			// (get) Token: 0x0600569B RID: 22171 RVA: 0x001655D0 File Offset: 0x001637D0
			internal IEnumerable<StorePropertyDefinition> RequestedProperties
			{
				get
				{
					foreach (StorePropertyDefinition propDef in this.requestedProperties)
					{
						if (this.IsPropertyRequestVisible(propDef))
						{
							yield return propDef;
						}
					}
					yield break;
				}
			}

			// Token: 0x0600569C RID: 22172 RVA: 0x001655ED File Offset: 0x001637ED
			public void ApplyResult()
			{
				if (this.resultSetter != null)
				{
					this.resultSetter(new Result<Participant>((this.Result != null) ? this.Result.ToParticipant() : null, this.error));
				}
			}

			// Token: 0x0600569D RID: 22173 RVA: 0x00165623 File Offset: 0x00163823
			internal bool AreSomePropertiesRequested(params StorePropertyDefinition[] propDefs)
			{
				return this.AreSomePropertiesRequested((IEnumerable<StorePropertyDefinition>)propDefs);
			}

			// Token: 0x0600569E RID: 22174 RVA: 0x00165634 File Offset: 0x00163834
			internal bool AreSomePropertiesRequested(IEnumerable<StorePropertyDefinition> propDefs)
			{
				foreach (StorePropertyDefinition propDef in propDefs)
				{
					if (this.IsPropertyRequested(propDef))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600569F RID: 22175 RVA: 0x00165688 File Offset: 0x00163888
			internal void IgnoreError()
			{
				this.inactiveBecauseOfError = false;
			}

			// Token: 0x060056A0 RID: 22176 RVA: 0x00165691 File Offset: 0x00163891
			internal bool IsPropertyRequested(StorePropertyDefinition propDef)
			{
				return this.requestedProperties.Contains(propDef) && this.IsPropertyRequestVisible(propDef);
			}

			// Token: 0x060056A1 RID: 22177 RVA: 0x001656AC File Offset: 0x001638AC
			internal void RequestProperties(IEnumerable<StorePropertyDefinition> propDefs)
			{
				foreach (StorePropertyDefinition propDef in propDefs)
				{
					this.RequestProperty(propDef);
				}
			}

			// Token: 0x060056A2 RID: 22178 RVA: 0x001656F4 File Offset: 0x001638F4
			internal void RequestProperty(StorePropertyDefinition propDef)
			{
				this.requestedProperties.TryAdd(propDef);
			}

			// Token: 0x060056A3 RID: 22179 RVA: 0x00165704 File Offset: 0x00163904
			internal T GetValueOrDefault<T>(StorePropertyDefinition propertyDefinition)
			{
				return this.GetValueOrDefault<T>(propertyDefinition, default(T));
			}

			// Token: 0x060056A4 RID: 22180 RVA: 0x00165724 File Offset: 0x00163924
			internal T GetValueOrDefault<T>(StorePropertyDefinition propertyDefinition, T defaultPropertyValue)
			{
				object propertyValue = this.result.TryGetProperty(propertyDefinition);
				if (PropertyError.IsPropertyNotFound(propertyValue))
				{
					propertyValue = this.input.TryGetProperty(propertyDefinition);
				}
				return PropertyBag.CheckPropertyValue<T>(propertyDefinition, propertyValue, defaultPropertyValue);
			}

			// Token: 0x060056A5 RID: 22181 RVA: 0x0016575B File Offset: 0x0016395B
			internal void InternalSetError(ProviderError error)
			{
				if (error == null)
				{
					throw new ArgumentNullException("error");
				}
				this.error = error;
				this.inactiveBecauseOfError = true;
			}

			// Token: 0x060056A6 RID: 22182 RVA: 0x00165779 File Offset: 0x00163979
			internal void SetOrDeleteProperty(StorePropertyDefinition propertyDefinition, object propertyValue)
			{
				this.result.SetOrDeleteProperty(propertyDefinition, propertyValue);
				if (!PropertyError.IsPropertyNotFound(propertyValue))
				{
					this.requestedProperties.Remove(propertyDefinition);
				}
			}

			// Token: 0x060056A7 RID: 22183 RVA: 0x0016579D File Offset: 0x0016399D
			internal void SetOrDeleteRequestedProperty(StorePropertyDefinition propertyDefinition, object propertyValue)
			{
				if (this.requestedProperties.Contains(propertyDefinition))
				{
					this.SetOrDeleteProperty(propertyDefinition, propertyValue);
				}
			}

			// Token: 0x060056A8 RID: 22184 RVA: 0x001657B5 File Offset: 0x001639B5
			private bool IsPropertyRequestVisible(StorePropertyDefinition propDef)
			{
				return (propDef.PropertyFlags & PropertyFlags.ReadOnly) == PropertyFlags.None || PropertyError.IsPropertyNotFound(this.result.TryGetProperty(propDef));
			}

			// Token: 0x04002E45 RID: 11845
			private readonly Participant input;

			// Token: 0x04002E46 RID: 11846
			private readonly HashSet<StorePropertyDefinition> requestedProperties = new HashSet<StorePropertyDefinition>();

			// Token: 0x04002E47 RID: 11847
			private readonly Participant.Builder result = new Participant.Builder();

			// Token: 0x04002E48 RID: 11848
			private readonly Action<Result<Participant>> resultSetter;

			// Token: 0x04002E49 RID: 11849
			private bool inactiveBecauseOfError;

			// Token: 0x04002E4A RID: 11850
			private ProviderError error;
		}

		// Token: 0x02000901 RID: 2305
		internal sealed class BatchBuilderError : ProviderError
		{
			// Token: 0x060056A9 RID: 22185 RVA: 0x001657D4 File Offset: 0x001639D4
			internal BatchBuilderError(Participant.BatchBuilder batchBuilder, string description, ProviderError innerError)
			{
				if (batchBuilder == null)
				{
					throw new ArgumentNullException("batchBuilder");
				}
				this.batchBuilder = batchBuilder;
				this.description = description;
				this.innerError = innerError;
			}

			// Token: 0x17001831 RID: 6193
			// (get) Token: 0x060056AA RID: 22186 RVA: 0x001657FF File Offset: 0x001639FF
			public Participant.BatchBuilder BatchBuilder
			{
				get
				{
					return this.batchBuilder;
				}
			}

			// Token: 0x17001832 RID: 6194
			// (get) Token: 0x060056AB RID: 22187 RVA: 0x00165807 File Offset: 0x00163A07
			public string Description
			{
				get
				{
					return this.description;
				}
			}

			// Token: 0x17001833 RID: 6195
			// (get) Token: 0x060056AC RID: 22188 RVA: 0x0016580F File Offset: 0x00163A0F
			public ProviderError InnerError
			{
				get
				{
					return this.innerError;
				}
			}

			// Token: 0x060056AD RID: 22189 RVA: 0x00165817 File Offset: 0x00163A17
			public override string ToString()
			{
				return ServerStrings.ExBatchBuilderError(this.batchBuilder, this.description);
			}

			// Token: 0x04002E4B RID: 11851
			private readonly Participant.BatchBuilder batchBuilder;

			// Token: 0x04002E4C RID: 11852
			private readonly string description;

			// Token: 0x04002E4D RID: 11853
			private readonly ProviderError innerError;
		}

		// Token: 0x02000902 RID: 2306
		internal class Job : Collection<Participant.JobItem>
		{
			// Token: 0x060056AE RID: 22190 RVA: 0x0016582F File Offset: 0x00163A2F
			public Job()
			{
			}

			// Token: 0x060056AF RID: 22191 RVA: 0x00165837 File Offset: 0x00163A37
			public Job(IList<Participant.JobItem> jobItems) : base(jobItems)
			{
			}

			// Token: 0x060056B0 RID: 22192 RVA: 0x00165840 File Offset: 0x00163A40
			public Job(int capacity)
			{
				((List<Participant.JobItem>)base.Items).Capacity = capacity;
			}

			// Token: 0x060056B1 RID: 22193 RVA: 0x0016585C File Offset: 0x00163A5C
			public Job(ICollection<Participant> jobItemInputs) : this(jobItemInputs.Count)
			{
				foreach (Participant input in jobItemInputs)
				{
					base.Add(new Participant.JobItem(input));
				}
			}

			// Token: 0x060056B2 RID: 22194 RVA: 0x001658B8 File Offset: 0x00163AB8
			public void ApplyResults()
			{
				foreach (Participant.JobItem jobItem in this)
				{
					jobItem.ApplyResult();
				}
			}

			// Token: 0x060056B3 RID: 22195 RVA: 0x00165A90 File Offset: 0x00163C90
			internal IEnumerable<Participant.JobItem> GetActiveJobItems()
			{
				foreach (Participant.JobItem jobItem in this)
				{
					if (jobItem.IsActive)
					{
						yield return jobItem;
					}
				}
				yield break;
			}

			// Token: 0x060056B4 RID: 22196 RVA: 0x00165AAD File Offset: 0x00163CAD
			protected override void InsertItem(int index, Participant.JobItem item)
			{
				if (item == null)
				{
					throw new ArgumentNullException("item");
				}
				base.InsertItem(index, item);
			}

			// Token: 0x060056B5 RID: 22197 RVA: 0x00165AC5 File Offset: 0x00163CC5
			protected override void SetItem(int index, Participant.JobItem item)
			{
				if (item == null)
				{
					throw new ArgumentNullException("item");
				}
				base.SetItem(index, item);
			}
		}
	}
}
