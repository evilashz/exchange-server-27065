using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000675 RID: 1653
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ArrayOfTransitionsType
	{
		// Token: 0x060032CA RID: 13002 RVA: 0x000B80B7 File Offset: 0x000B62B7
		public ArrayOfTransitionsType()
		{
		}

		// Token: 0x060032CB RID: 13003 RVA: 0x000B80BF File Offset: 0x000B62BF
		public ArrayOfTransitionsType(bool transitionsGroup, string id, TransitionType[] transitions) : this(transitionsGroup)
		{
			this.Id = id;
			this.Transition = transitions;
		}

		// Token: 0x060032CC RID: 13004 RVA: 0x000B80D6 File Offset: 0x000B62D6
		internal ArrayOfTransitionsType(bool transitionsGroup)
		{
			this.name = (transitionsGroup ? "TransitionsGroup" : "Transitions");
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x060032CD RID: 13005 RVA: 0x000B80F3 File Offset: 0x000B62F3
		// (set) Token: 0x060032CE RID: 13006 RVA: 0x000B80FB File Offset: 0x000B62FB
		[XmlElement("RecurringDayTransition", typeof(RecurringDayTransitionType))]
		[XmlElement("RecurringDateTransition", typeof(RecurringDateTransitionType))]
		[XmlElement("Transition", typeof(TransitionType))]
		[DataMember(EmitDefaultValue = false, Order = 1)]
		[XmlElement("AbsoluteDateTransition", typeof(AbsoluteDateTransitionType))]
		public TransitionType[] Transition { get; set; }

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x060032CF RID: 13007 RVA: 0x000B8104 File Offset: 0x000B6304
		// (set) Token: 0x060032D0 RID: 13008 RVA: 0x000B810C File Offset: 0x000B630C
		[DataMember(EmitDefaultValue = false, Order = 0)]
		[XmlAttribute]
		public string Id { get; set; }

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x060032D1 RID: 13009 RVA: 0x000B8115 File Offset: 0x000B6315
		[IgnoreDataMember]
		[XmlIgnore]
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x060032D2 RID: 13010 RVA: 0x000B8120 File Offset: 0x000B6320
		internal void Add(TransitionType transition)
		{
			if (this.Transition != null)
			{
				TransitionType[] array = new TransitionType[this.Transition.Length + 1];
				this.Transition.CopyTo(array, 0);
				array[this.Transition.Length] = transition;
				this.Transition = array;
				return;
			}
			this.Transition = new TransitionType[]
			{
				transition
			};
		}

		// Token: 0x04001CBD RID: 7357
		private const string TransitionsGroupString = "TransitionsGroup";

		// Token: 0x04001CBE RID: 7358
		private const string TransitionsString = "Transitions";

		// Token: 0x04001CBF RID: 7359
		private readonly string name;
	}
}
