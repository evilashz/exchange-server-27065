using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AE2 RID: 2786
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class AcrHelpers
	{
		// Token: 0x06006506 RID: 25862 RVA: 0x001ACC70 File Offset: 0x001AAE70
		internal static object[] ResolveToClientValue(IList<AcrPropertyProfile.ValuesToResolve> valuesToResolve, IList<AcrPropertyProfile.ValuesToResolve> dependencies)
		{
			return new object[]
			{
				valuesToResolve[0].ClientValue
			};
		}

		// Token: 0x06006507 RID: 25863 RVA: 0x001ACC98 File Offset: 0x001AAE98
		internal static object[] ResolveToClientValueIfServerValueNotModified(IList<AcrPropertyProfile.ValuesToResolve> valuesToResolve, IList<AcrPropertyProfile.ValuesToResolve> dependencies)
		{
			if (AcrHelpers.MatchPropertyValues(valuesToResolve[0].ServerValue, valuesToResolve[0].OriginalValue))
			{
				return new object[]
				{
					valuesToResolve[0].ClientValue
				};
			}
			if (AcrHelpers.MatchPropertyValues(valuesToResolve[0].ServerValue, valuesToResolve[0].ClientValue))
			{
				return new object[]
				{
					valuesToResolve[0].ClientValue
				};
			}
			return null;
		}

		// Token: 0x06006508 RID: 25864 RVA: 0x001ACD14 File Offset: 0x001AAF14
		internal static object[] ResolveToOredValue(IList<AcrPropertyProfile.ValuesToResolve> valuesToResolve, IList<AcrPropertyProfile.ValuesToResolve> dependencies)
		{
			object[] result;
			if (valuesToResolve[0].ClientValue is bool && valuesToResolve[0].ServerValue is bool)
			{
				result = new object[]
				{
					(bool)valuesToResolve[0].ClientValue || (bool)valuesToResolve[0].ServerValue
				};
			}
			else if (valuesToResolve[0].ClientValue is bool)
			{
				result = new object[]
				{
					valuesToResolve[0].ClientValue
				};
			}
			else
			{
				result = new object[]
				{
					valuesToResolve[0].ServerValue
				};
			}
			return result;
		}

		// Token: 0x06006509 RID: 25865 RVA: 0x001ACDC8 File Offset: 0x001AAFC8
		internal static object[] ResolveToEarlierTime(IList<AcrPropertyProfile.ValuesToResolve> valuesToResolve, IList<AcrPropertyProfile.ValuesToResolve> dependencies)
		{
			object[] array = new object[1];
			if (valuesToResolve[0].ClientValue is ExDateTime && valuesToResolve[0].ServerValue is ExDateTime)
			{
				array[0] = (((ExDateTime)valuesToResolve[0].ClientValue < (ExDateTime)valuesToResolve[0].ServerValue) ? valuesToResolve[0].ClientValue : valuesToResolve[0].ServerValue);
			}
			else if (valuesToResolve[0].ClientValue is ExDateTime)
			{
				array[0] = valuesToResolve[0].ClientValue;
			}
			else
			{
				array[0] = valuesToResolve[0].ServerValue;
			}
			return array;
		}

		// Token: 0x0600650A RID: 25866 RVA: 0x001ACE80 File Offset: 0x001AB080
		internal static object[] ResolveToHighestIntValue(IList<AcrPropertyProfile.ValuesToResolve> valuesToResolve, IList<AcrPropertyProfile.ValuesToResolve> dependencies)
		{
			object[] array = new object[1];
			if (valuesToResolve[0].ClientValue is int && valuesToResolve[0].ServerValue is int)
			{
				array[0] = Math.Max((int)valuesToResolve[0].ClientValue, (int)valuesToResolve[0].ServerValue);
			}
			else if (valuesToResolve[0].ClientValue is int)
			{
				array[0] = valuesToResolve[0].ClientValue;
			}
			else
			{
				array[0] = valuesToResolve[0].ServerValue;
			}
			return array;
		}

		// Token: 0x0600650B RID: 25867 RVA: 0x001ACF20 File Offset: 0x001AB120
		internal static object[] ResolveToIncrementHighestIntValue(IList<AcrPropertyProfile.ValuesToResolve> valuesToResolve, IList<AcrPropertyProfile.ValuesToResolve> dependencies)
		{
			object[] array = AcrHelpers.ResolveToHighestIntValue(valuesToResolve, dependencies);
			if (array.Length > 0 && array[0] is int)
			{
				int num = (int)array[0];
				array[0] = num + 1;
			}
			return array;
		}

		// Token: 0x0600650C RID: 25868 RVA: 0x001ACF5C File Offset: 0x001AB15C
		internal static object[] ResolveResponseType(IList<AcrPropertyProfile.ValuesToResolve> valuesToResolve, IList<AcrPropertyProfile.ValuesToResolve> dependencies)
		{
			object[] result = null;
			int num = 5;
			if (valuesToResolve[0].ServerValue != null)
			{
				num = (int)valuesToResolve[0].ServerValue;
			}
			if (num == 5)
			{
				result = new object[]
				{
					valuesToResolve[0].ClientValue
				};
			}
			return result;
		}

		// Token: 0x0600650D RID: 25869 RVA: 0x001ACFAC File Offset: 0x001AB1AC
		internal static object[] ResolveToHighestPriorityAndImportance(IList<AcrPropertyProfile.ValuesToResolve> valuesToResolve, IList<AcrPropertyProfile.ValuesToResolve> dependencies)
		{
			object[] array = new object[2];
			int num = int.MinValue;
			if (valuesToResolve[0].ServerValue is int)
			{
				num = Math.Max(1 + (int)valuesToResolve[0].ServerValue, num);
			}
			if (valuesToResolve[0].ClientValue is int)
			{
				num = Math.Max(1 + (int)valuesToResolve[0].ClientValue, num);
			}
			if (valuesToResolve[1].ServerValue is int)
			{
				num = Math.Max((int)valuesToResolve[1].ServerValue, num);
			}
			if (valuesToResolve[1].ClientValue is int)
			{
				num = Math.Max((int)valuesToResolve[1].ClientValue, num);
			}
			if (num != -2147483648)
			{
				array[0] = num - 1;
				array[1] = num;
			}
			else
			{
				array[0] = valuesToResolve[0].ClientValue;
				array[1] = valuesToResolve[1].ClientValue;
			}
			return array;
		}

		// Token: 0x0600650E RID: 25870 RVA: 0x001AD0B4 File Offset: 0x001AB2B4
		internal static object[] ResolveToHighestSensitivity(IList<AcrPropertyProfile.ValuesToResolve> valuesToResolve, IList<AcrPropertyProfile.ValuesToResolve> dependencies)
		{
			object[] array = new object[2];
			int num = int.MinValue;
			if (valuesToResolve[0].ServerValue is int)
			{
				num = Math.Max((int)valuesToResolve[0].ServerValue, num);
			}
			if (valuesToResolve[0].ClientValue is int)
			{
				num = Math.Max((int)valuesToResolve[0].ClientValue, num);
			}
			if (num != -2147483648)
			{
				array[0] = num;
				array[1] = ((num == 2) ? true : valuesToResolve[1].ClientValue);
			}
			else
			{
				array[0] = valuesToResolve[0].ClientValue;
				array[1] = valuesToResolve[1].ClientValue;
			}
			return array;
		}

		// Token: 0x0600650F RID: 25871 RVA: 0x001AD174 File Offset: 0x001AB374
		internal static object[] ResolveToMergedStringValues(IList<AcrPropertyProfile.ValuesToResolve> values, IList<AcrPropertyProfile.ValuesToResolve> dependencies)
		{
			string[] array = values[0].ClientValue as string[];
			string[] array2 = values[0].ServerValue as string[];
			object[] array3 = null;
			if (array2 != null && array != null)
			{
				SortedDictionary<string, string> sortedDictionary = new SortedDictionary<string, string>();
				foreach (string text in array2)
				{
					sortedDictionary[text] = text;
				}
				foreach (string text2 in array)
				{
					if (!sortedDictionary.ContainsKey(text2))
					{
						sortedDictionary.Add(text2, text2);
					}
				}
				if (sortedDictionary.Count > 0)
				{
					array3 = new object[]
					{
						new string[sortedDictionary.Count]
					};
					sortedDictionary.Keys.CopyTo((string[])array3[0], 0);
				}
			}
			else if (array != null)
			{
				array3 = new object[]
				{
					array
				};
			}
			else
			{
				array3 = new object[]
				{
					values[0].ServerValue
				};
			}
			return array3;
		}

		// Token: 0x06006510 RID: 25872 RVA: 0x001AD27C File Offset: 0x001AB47C
		internal static object[] ResolveToServerValueIfClientMatchesServer(IList<AcrPropertyProfile.ValuesToResolve> valuesToResolve, IList<AcrPropertyProfile.ValuesToResolve> dependencies)
		{
			if (AcrHelpers.MatchPropertyValues(valuesToResolve[0].ServerValue, valuesToResolve[0].ClientValue))
			{
				return new object[]
				{
					valuesToResolve[0].ServerValue
				};
			}
			return null;
		}

		// Token: 0x06006511 RID: 25873 RVA: 0x001AD2C4 File Offset: 0x001AB4C4
		internal static object[] ResolveToModifiedValue(IList<AcrPropertyProfile.ValuesToResolve> valuesToResolve, IList<AcrPropertyProfile.ValuesToResolve> dependencies)
		{
			if (valuesToResolve[0].OriginalValue == null)
			{
				return null;
			}
			bool flag = !AcrHelpers.MatchPropertyValues(valuesToResolve[0].ClientValue, valuesToResolve[0].OriginalValue);
			bool flag2 = !AcrHelpers.MatchPropertyValues(valuesToResolve[0].ServerValue, valuesToResolve[0].OriginalValue);
			if (flag && !flag2)
			{
				return new object[]
				{
					valuesToResolve[0].ClientValue
				};
			}
			if (!flag)
			{
				return new object[]
				{
					valuesToResolve[0].ServerValue
				};
			}
			return null;
		}

		// Token: 0x06006512 RID: 25874 RVA: 0x001AD363 File Offset: 0x001AB563
		internal static object[] ResolveToHighestValue<T>(IList<AcrPropertyProfile.ValuesToResolve> valuesToResolve, IList<AcrPropertyProfile.ValuesToResolve> dependencies) where T : IComparable<T>
		{
			return AcrHelpers.ResolveBasedOnCompare<T>((int sign) => sign > 0, valuesToResolve, dependencies);
		}

		// Token: 0x06006513 RID: 25875 RVA: 0x001AD378 File Offset: 0x001AB578
		internal static object[] ResolveToGreatestDependency<T>(IList<AcrPropertyProfile.ValuesToResolve> valuesToResolve, IList<AcrPropertyProfile.ValuesToResolve> dependencies) where T : IComparable<T>
		{
			AcrPropertyProfile.ValuesToResolve valuesToResolve2 = dependencies[0];
			bool flag;
			if (valuesToResolve2.ServerValue is T)
			{
				if (valuesToResolve2.ClientValue is T)
				{
					T t = (T)((object)valuesToResolve2.ClientValue);
					flag = (t.CompareTo((T)((object)valuesToResolve2.ServerValue)) >= 0);
				}
				else
				{
					flag = false;
				}
			}
			else
			{
				flag = true;
			}
			bool flag2 = flag;
			object[] array = new object[valuesToResolve.Count];
			for (int i = 0; i < valuesToResolve.Count; i++)
			{
				array[i] = (flag2 ? valuesToResolve[i].ClientValue : valuesToResolve[i].ServerValue);
			}
			return array;
		}

		// Token: 0x06006514 RID: 25876 RVA: 0x001AD418 File Offset: 0x001AB618
		internal static bool MatchPropertyValues(object propValue1, object propValue2)
		{
			PropertyError propertyError = propValue1 as PropertyError;
			PropertyError propertyError2 = propValue2 as PropertyError;
			return (propertyError == null && Util.ValueEquals(propValue1, propValue2)) || (propertyError != null && propertyError2 != null && propertyError.PropertyErrorCode == PropertyErrorCode.NotFound && propertyError2.PropertyErrorCode == PropertyErrorCode.NotFound);
		}

		// Token: 0x06006515 RID: 25877 RVA: 0x001AD45C File Offset: 0x001AB65C
		private static object[] ResolveBasedOnCompare<T>(Predicate<int> comparisonInterpreter, IList<AcrPropertyProfile.ValuesToResolve> valuesToResolve, IList<AcrPropertyProfile.ValuesToResolve> dependencies) where T : IComparable<T>
		{
			AcrPropertyProfile.ValuesToResolve valuesToResolve2 = valuesToResolve[0];
			bool flag;
			if (AcrHelpers.MatchPropertyValues(valuesToResolve2.ClientValue, valuesToResolve2.ServerValue))
			{
				flag = comparisonInterpreter(0);
			}
			else if (!(valuesToResolve2.ServerValue is T))
			{
				flag = comparisonInterpreter(1);
			}
			else if (!(valuesToResolve2.ClientValue is T))
			{
				flag = comparisonInterpreter(-1);
			}
			else
			{
				T t = (T)((object)valuesToResolve2.ClientValue);
				flag = comparisonInterpreter(t.CompareTo((T)((object)valuesToResolve2.ServerValue)));
			}
			return new object[]
			{
				flag ? valuesToResolve2.ClientValue : valuesToResolve2.ServerValue
			};
		}
	}
}
