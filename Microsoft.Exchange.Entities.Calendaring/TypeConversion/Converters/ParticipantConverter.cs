using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.Entities.TypeConversion.Converters;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters
{
	// Token: 0x02000070 RID: 112
	internal abstract class ParticipantConverter<TStorageType, TParticipantWrapper, TRecipient> : IConverter<TStorageType, TRecipient> where TParticipantWrapper : ParticipantWrapper<TStorageType> where TRecipient : class, IRecipient, ISchematizedObject<RecipientSchema>, new()
	{
		// Token: 0x060002EE RID: 750 RVA: 0x0000AE77 File Offset: 0x00009077
		protected ParticipantConverter(IParticipantRoutingTypeConverter routingTypeConverter)
		{
			this.routingTypeConverter = routingTypeConverter.AssertNotNull("routingTypeConverter");
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000AE90 File Offset: 0x00009090
		protected virtual IParticipantRoutingTypeConverter RoutingTypeConverter
		{
			get
			{
				return this.routingTypeConverter;
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000AE98 File Offset: 0x00009098
		public TRecipient Convert(TStorageType value)
		{
			return this.Convert(new TStorageType[]
			{
				value
			}).First<TRecipient>();
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000AEC0 File Offset: 0x000090C0
		public Participant Convert(TRecipient value)
		{
			if (value == null)
			{
				throw new ExArgumentNullException("value");
			}
			return new Participant(value.Name, value.EmailAddress, this.GetRoutingType(value));
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000AF6C File Offset: 0x0000916C
		public virtual IEnumerable<TRecipient> Convert(IEnumerable<TStorageType> wrappables)
		{
			if (wrappables == null)
			{
				return null;
			}
			TParticipantWrapper[] wrappedParticipants = (from storageObject in wrappables
			select this.WrapStorageType(storageObject)).ToArray<TParticipantWrapper>();
			Participant[] value = (from wrapper in wrappedParticipants
			select wrapper.Participant).ToArray<Participant>();
			IEnumerable<Participant> source = this.routingTypeConverter.ConvertToEntity(value);
			int index = -1;
			return from participant in source
			select this.CopyFromParticipant(participant, wrappedParticipants[++index].Original);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000B004 File Offset: 0x00009204
		protected virtual TRecipient CopyFromParticipant(Participant value, TStorageType original)
		{
			if (value == null)
			{
				return default(TRecipient);
			}
			TRecipient result = Activator.CreateInstance<TRecipient>();
			result.EmailAddress = value.EmailAddress;
			result.Name = value.DisplayName;
			result.RoutingType = value.RoutingType;
			return result;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000B068 File Offset: 0x00009268
		protected string GetRoutingType(TRecipient recipient)
		{
			return recipient.IsPropertySet(recipient.Schema.RoutingTypeProperty) ? recipient.RoutingType : "SMTP";
		}

		// Token: 0x060002F5 RID: 757
		protected abstract TParticipantWrapper WrapStorageType(TStorageType value);

		// Token: 0x040000D2 RID: 210
		private readonly IParticipantRoutingTypeConverter routingTypeConverter;
	}
}
