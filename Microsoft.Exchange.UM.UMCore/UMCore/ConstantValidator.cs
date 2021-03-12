using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020000FD RID: 253
	internal class ConstantValidator
	{
		// Token: 0x060006B0 RID: 1712 RVA: 0x0001AFD1 File Offset: 0x000191D1
		private ConstantValidator()
		{
			ConstantValidator.cache = new Hashtable();
			this.AddConstantsToCache(typeof(Constants));
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0001AFF3 File Offset: 0x000191F3
		internal static ConstantValidator Instance
		{
			get
			{
				if (ConstantValidator.instance == null)
				{
					ConstantValidator.instance = new ConstantValidator();
				}
				return ConstantValidator.instance;
			}
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0001B00B File Offset: 0x0001920B
		internal static void Release()
		{
			ConstantValidator.instance = null;
			ConstantValidator.cache = null;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0001B019 File Offset: 0x00019219
		internal bool ValidateVariableName(string varname)
		{
			return ConstantValidator.ValidateConstant(typeof(Constants.VariableName), varname);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001B02B File Offset: 0x0001922B
		internal bool ValidateCondition(string varname)
		{
			return ConstantValidator.ValidateConstant(typeof(Constants.Condition), varname) || ConstantValidator.ValidateConstant(typeof(Constants.VariableName), varname);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001B051 File Offset: 0x00019251
		internal bool ValidateAction(string varname)
		{
			return ConstantValidator.ValidateConstant(typeof(Constants.Action), varname);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001B063 File Offset: 0x00019263
		internal bool ValidateEvent(string varname)
		{
			return Regex.IsMatch(varname, "[0-9#*A-D]+") || ConstantValidator.ValidateConstant(typeof(Constants.TransitionEvent), varname) || ConstantValidator.ValidateConstant(typeof(Constants.RecoEvent), varname);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001B096 File Offset: 0x00019296
		internal bool ValidateRecoEvent(string varname)
		{
			return ConstantValidator.ValidateConstant(typeof(Constants.RecoEvent), varname);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0001B0A8 File Offset: 0x000192A8
		internal bool ValidatePromptLimit(string varname)
		{
			return ConstantValidator.ValidateConstant(typeof(Constants.PromptLimits), varname);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0001B0BC File Offset: 0x000192BC
		private static bool ValidateConstant(Type outerType, string varname)
		{
			ArrayList arrayList = ConstantValidator.cache[varname] as ArrayList;
			string value = outerType.ToString();
			if (arrayList == null)
			{
				return false;
			}
			foreach (object obj in arrayList)
			{
				string text = (string)obj;
				if (text.StartsWith(value, false, CultureInfo.InvariantCulture))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001B144 File Offset: 0x00019344
		private void AddConstantsToCache(Type outerType)
		{
			BindingFlags bindingAttr = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
			FieldInfo[] fields = outerType.GetFields(bindingAttr);
			foreach (FieldInfo fieldInfo in fields)
			{
				if (typeof(string) == fieldInfo.FieldType && fieldInfo.IsLiteral)
				{
					string key = (string)fieldInfo.GetValue(null);
					if (!ConstantValidator.cache.ContainsKey(key))
					{
						ConstantValidator.cache[key] = new ArrayList();
					}
					((ArrayList)ConstantValidator.cache[key]).Add(outerType.ToString());
				}
			}
			Type[] nestedTypes = outerType.GetNestedTypes(bindingAttr);
			foreach (Type outerType2 in nestedTypes)
			{
				this.AddConstantsToCache(outerType2);
			}
		}

		// Token: 0x040007CD RID: 1997
		private static ConstantValidator instance;

		// Token: 0x040007CE RID: 1998
		private static Hashtable cache;
	}
}
