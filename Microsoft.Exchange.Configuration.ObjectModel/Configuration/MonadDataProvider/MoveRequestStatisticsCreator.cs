using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001C7 RID: 455
	internal class MoveRequestStatisticsCreator : MockObjectCreator
	{
		// Token: 0x06001003 RID: 4099 RVA: 0x000308BD File Offset: 0x0002EABD
		internal override object CreateDummyObject(Type type)
		{
			return base.CreateDummyObject(typeof(MoveRequestStatisticsPresentationObject));
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x000308D0 File Offset: 0x0002EAD0
		internal override IList<string> GetProperties(string fullName)
		{
			return new string[]
			{
				"DisplayName",
				"Status",
				"PercentComplete",
				"OverallDuration",
				"TotalMailboxSize",
				"BadItemsEncountered",
				"LastUpdateTimestamp",
				"SuspendWhenReadyToComplete",
				"IsOffline",
				"RemoteHostName",
				"MRSServerName",
				"SourceVersion",
				"SourceDatabase",
				"TargetVersion",
				"TargetDatabase",
				"QueuedTimestamp",
				"TotalQueuedDuration",
				"StartTimestamp",
				"CompletionTimestamp",
				"SuspendedTimestamp",
				"Message",
				"Flags",
				"Report"
			};
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x000309AC File Offset: 0x0002EBAC
		protected override void FillProperties(Type type, PSObject psObject, object dummyObject, IList<string> properties)
		{
			MoveRequestStatisticsPresentationObject moveRequestStatisticsPresentationObject = dummyObject as MoveRequestStatisticsPresentationObject;
			foreach (PSMemberInfo psmemberInfo in psObject.Members)
			{
				if (properties.Contains(psmemberInfo.Name))
				{
					if (psmemberInfo.Name == "Report")
					{
						PSObject psobject = psObject.Members[psmemberInfo.Name].Value as PSObject;
						if (psobject != null)
						{
							Type type2 = MonadCommand.ResolveType(psobject);
							BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
							ConstructorInfo constructor = type2.GetConstructor(bindingAttr, null, new Type[]
							{
								typeof(Guid)
							}, null);
							object obj = constructor.Invoke(new object[]
							{
								Guid.Empty
							});
							object value = psobject.Members["Entries"].Value;
							if (value is PSObject && ((PSObject)value).BaseObject is IList)
							{
								IList list = ((PSObject)value).BaseObject as IList;
								foreach (object obj2 in list)
								{
									type2.GetMethod("Append", bindingAttr, null, new Type[]
									{
										typeof(LocalizedString)
									}, null).Invoke(obj, new object[]
									{
										new LocalizedString(obj2.ToString())
									});
								}
							}
							PropertyInfo property = moveRequestStatisticsPresentationObject.GetType().GetProperty(psmemberInfo.Name);
							property.SetValue(moveRequestStatisticsPresentationObject, obj, null);
						}
					}
					else
					{
						PropertyInfo property2 = moveRequestStatisticsPresentationObject.GetType().GetProperty(psmemberInfo.Name);
						property2.SetValue(moveRequestStatisticsPresentationObject, MockObjectCreator.GetSingleProperty(psObject.Members[psmemberInfo.Name].Value, property2.PropertyType), null);
					}
				}
			}
		}
	}
}
