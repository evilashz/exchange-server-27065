using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000065 RID: 101
	[Get(typeof(OnlineMeetingExtensionResource))]
	[Parent("onlineMeeting")]
	[DataContract(Name = "OnlineMeetingExtensionResource")]
	internal class OnlineMeetingExtensionResource : Resource
	{
		// Token: 0x060002DC RID: 732 RVA: 0x00009499 File Offset: 0x00007699
		public OnlineMeetingExtensionResource(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060002DD RID: 733 RVA: 0x000094A2 File Offset: 0x000076A2
		// (set) Token: 0x060002DE RID: 734 RVA: 0x000094AF File Offset: 0x000076AF
		[DataMember(Name = "id", EmitDefaultValue = false)]
		public string Id
		{
			get
			{
				return base.GetValue<string>("Id");
			}
			set
			{
				base.SetValue<string>("Id", value);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060002DF RID: 735 RVA: 0x000094BD File Offset: 0x000076BD
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x000094CA File Offset: 0x000076CA
		[DataMember(Name = "type", EmitDefaultValue = false)]
		public OnlineMeetingExtensionType? Type
		{
			get
			{
				return base.GetValue<OnlineMeetingExtensionType?>("type");
			}
			set
			{
				base.SetValue<OnlineMeetingExtensionType?>("type", value);
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x000094DD File Offset: 0x000076DD
		public ICollection<string> PropertyNames
		{
			get
			{
				return base.Keys;
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x000094E5 File Offset: 0x000076E5
		public T GetPropertyValue<T>(string name)
		{
			return base.GetValue<T>(name);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x000094EE File Offset: 0x000076EE
		public void SetPropertyValue<T>(string name, T value)
		{
			base.SetValue<T>(name, value);
		}

		// Token: 0x040001D9 RID: 473
		public const string Token = "onlineMeetingExtension";
	}
}
