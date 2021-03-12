using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003F2 RID: 1010
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PreservableMeetingMessageProperty
	{
		// Token: 0x06002E20 RID: 11808 RVA: 0x000BD89C File Offset: 0x000BBA9C
		static PreservableMeetingMessageProperty()
		{
			PreservableMeetingMessageProperty.CreatePreservableProperty(InternalSchema.Location, new ShouldPreservePropertyDelegate(PreservableMeetingMessageProperty.PreserveLocationTest));
			PreservableMeetingMessageProperty.CreatePreservableProperty(InternalSchema.Subject, new ShouldPreservePropertyDelegate(PreservableMeetingMessageProperty.IsRumTest));
			PreservableMeetingMessageProperty.CreatePreservableProperty(InternalSchema.SubjectPrefix, new ShouldPreservePropertyDelegate(PreservableMeetingMessageProperty.IsRumTest));
			PreservableMeetingMessageProperty.CreatePreservableProperty(InternalSchema.NormalizedSubject, new ShouldPreservePropertyDelegate(PreservableMeetingMessageProperty.IsRumTest));
		}

		// Token: 0x06002E21 RID: 11809 RVA: 0x000BD90F File Offset: 0x000BBB0F
		private PreservableMeetingMessageProperty(StorePropertyDefinition propDef, ShouldPreservePropertyDelegate shouldPreserveTest, CopyPropertyDelegate copyMethod)
		{
			this.PropertyDefinition = propDef;
			this.ShouldPreserve = shouldPreserveTest;
			this.CopyProperty = copyMethod;
			PreservableMeetingMessageProperty.InstanceFromPropertyDefinition[propDef] = this;
		}

		// Token: 0x06002E22 RID: 11810 RVA: 0x000BD938 File Offset: 0x000BBB38
		private static PreservableMeetingMessageProperty CreatePreservableProperty(StorePropertyDefinition propDef, ShouldPreservePropertyDelegate shouldPreserveTest, CopyPropertyDelegate copyMethod)
		{
			return new PreservableMeetingMessageProperty(propDef, shouldPreserveTest, copyMethod);
		}

		// Token: 0x06002E23 RID: 11811 RVA: 0x000BD942 File Offset: 0x000BBB42
		private static PreservableMeetingMessageProperty CreatePreservableProperty(StorePropertyDefinition propDef, ShouldPreservePropertyDelegate shouldPreserveTest)
		{
			return PreservableMeetingMessageProperty.CreatePreservableProperty(propDef, shouldPreserveTest, new CopyPropertyDelegate(PreservableMeetingMessageProperty.DefaultCopy));
		}

		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x06002E24 RID: 11812 RVA: 0x000BD957 File Offset: 0x000BBB57
		public static IEnumerable<PreservableMeetingMessageProperty> PreservableProperties
		{
			get
			{
				return PreservableMeetingMessageProperty.InstanceFromPropertyDefinition.Values;
			}
		}

		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x06002E25 RID: 11813 RVA: 0x000BD963 File Offset: 0x000BBB63
		public static IEnumerable<StorePropertyDefinition> PreservablePropertyDefinitions
		{
			get
			{
				return new List<StorePropertyDefinition>(PreservableMeetingMessageProperty.InstanceFromPropertyDefinition.Keys);
			}
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x000BD974 File Offset: 0x000BBB74
		public static void CopyPreserving(PreservablePropertyContext context)
		{
			List<PreservableMeetingMessageProperty> list = new List<PreservableMeetingMessageProperty>();
			foreach (PreservableMeetingMessageProperty preservableMeetingMessageProperty in PreservableMeetingMessageProperty.PreservableProperties)
			{
				if (!preservableMeetingMessageProperty.ShouldPreserve(context))
				{
					list.Add(preservableMeetingMessageProperty);
				}
			}
			foreach (PreservableMeetingMessageProperty preservableMeetingMessageProperty2 in list)
			{
				preservableMeetingMessageProperty2.Copy(context);
			}
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x000BDA14 File Offset: 0x000BBC14
		private static void DefaultCopy(PreservablePropertyContext context, StorePropertyDefinition prop)
		{
			CalendarItemBase.CopyPropertiesTo(context.MeetingRequest, context.CalendarItem, new PropertyDefinition[]
			{
				prop
			});
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x000BDA3E File Offset: 0x000BBC3E
		private static bool IsRumTest(PreservablePropertyContext context)
		{
			return context.MeetingRequest.IsRepairUpdateMessage;
		}

		// Token: 0x06002E29 RID: 11817 RVA: 0x000BDA4B File Offset: 0x000BBC4B
		private static bool PreserveLocationTest(PreservablePropertyContext context)
		{
			return context.MeetingRequest.IsRepairUpdateMessage && (context.OrganizerHighlights & ChangeHighlightProperties.Location) == ChangeHighlightProperties.None && PreservableMeetingMessageProperty.IsLocationConsistent(context);
		}

		// Token: 0x06002E2A RID: 11818 RVA: 0x000BDA6C File Offset: 0x000BBC6C
		private static bool IsLocationConsistent(PreservablePropertyContext context)
		{
			return context.CalendarItem.Location.Contains(context.MeetingRequest.Location);
		}

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x06002E2B RID: 11819 RVA: 0x000BDA89 File Offset: 0x000BBC89
		public static Dictionary<StorePropertyDefinition, PreservableMeetingMessageProperty> InstanceFromPropertyDefinition
		{
			get
			{
				return PreservableMeetingMessageProperty.propertiesDictionary;
			}
		}

		// Token: 0x06002E2C RID: 11820 RVA: 0x000BDA90 File Offset: 0x000BBC90
		private void Copy(PreservablePropertyContext context)
		{
			this.CopyProperty(context, this.PropertyDefinition);
		}

		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x06002E2D RID: 11821 RVA: 0x000BDAA4 File Offset: 0x000BBCA4
		// (set) Token: 0x06002E2E RID: 11822 RVA: 0x000BDAAC File Offset: 0x000BBCAC
		public ShouldPreservePropertyDelegate ShouldPreserve { get; private set; }

		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x06002E2F RID: 11823 RVA: 0x000BDAB5 File Offset: 0x000BBCB5
		// (set) Token: 0x06002E30 RID: 11824 RVA: 0x000BDABD File Offset: 0x000BBCBD
		private StorePropertyDefinition PropertyDefinition { get; set; }

		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x06002E31 RID: 11825 RVA: 0x000BDAC6 File Offset: 0x000BBCC6
		// (set) Token: 0x06002E32 RID: 11826 RVA: 0x000BDACE File Offset: 0x000BBCCE
		private CopyPropertyDelegate CopyProperty { get; set; }

		// Token: 0x04001925 RID: 6437
		private static Dictionary<StorePropertyDefinition, PreservableMeetingMessageProperty> propertiesDictionary = new Dictionary<StorePropertyDefinition, PreservableMeetingMessageProperty>();
	}
}
