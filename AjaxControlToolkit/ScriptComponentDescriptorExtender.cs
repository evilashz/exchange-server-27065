using System;
using System.Web.UI;

namespace AjaxControlToolkit
{
	// Token: 0x0200001B RID: 27
	public static class ScriptComponentDescriptorExtender
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x000033C2 File Offset: 0x000015C2
		public static void AddClientIdProperty(this ScriptComponentDescriptor descriptor, string name, Control control)
		{
			if (control == null || !control.Visible)
			{
				return;
			}
			descriptor.AddProperty(name, control.ClientID);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000033E0 File Offset: 0x000015E0
		public static void AddComponentProperty(this ScriptComponentDescriptor descriptor, string name, string value, IControlResolver controlResolver)
		{
			if (!string.IsNullOrEmpty(value))
			{
				Control control = null;
				if (controlResolver != null)
				{
					control = controlResolver.ResolveControl(value);
				}
				if (control != null)
				{
					ExtenderControlBase extenderControlBase = control as ExtenderControlBase;
					if (extenderControlBase != null && extenderControlBase.BehaviorID.Length > 0)
					{
						value = extenderControlBase.BehaviorID;
					}
					else
					{
						value = control.ClientID;
					}
				}
				descriptor.AddComponentProperty(name, value);
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003437 File Offset: 0x00001637
		public static void AddComponentProperty(this ScriptComponentDescriptor descriptor, string name, string value, bool skipWithEmptyOrNullValue)
		{
			if (skipWithEmptyOrNullValue && string.IsNullOrEmpty(value))
			{
				return;
			}
			descriptor.AddComponentProperty(name, value);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000344D File Offset: 0x0000164D
		public static void AddComponentProperty(this ScriptComponentDescriptor descriptor, string name, Control control)
		{
			if (control == null || !control.Visible)
			{
				return;
			}
			descriptor.AddComponentProperty(name, control.ClientID);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003468 File Offset: 0x00001668
		public static void AddElementProperty(this ScriptComponentDescriptor descriptor, string name, string value, IControlResolver controlResolver)
		{
			if (!string.IsNullOrEmpty(value))
			{
				Control control = null;
				if (controlResolver != null)
				{
					control = controlResolver.ResolveControl(value);
				}
				if (control != null)
				{
					value = control.ClientID;
				}
				descriptor.AddElementProperty(name, value);
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000349D File Offset: 0x0000169D
		public static void AddElementProperty(this ScriptComponentDescriptor descriptor, string name, string value, bool skipWithEmptyOrNullValue)
		{
			if (skipWithEmptyOrNullValue && string.IsNullOrEmpty(value))
			{
				return;
			}
			descriptor.AddElementProperty(name, value);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000034B3 File Offset: 0x000016B3
		public static void AddProperty(this ScriptComponentDescriptor descriptor, string name, string value, bool skipWithEmptyOrNullValue)
		{
			if (skipWithEmptyOrNullValue && string.IsNullOrEmpty(value))
			{
				return;
			}
			descriptor.AddProperty(name, value);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000034C9 File Offset: 0x000016C9
		public static void AddProperty(this ScriptComponentDescriptor descriptor, string name, bool value, bool skipFalseValue)
		{
			if (value || !skipFalseValue)
			{
				descriptor.AddProperty(name, value);
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000034DE File Offset: 0x000016DE
		public static void AddProperty(this ScriptComponentDescriptor descriptor, string name, int value, int defaultValue)
		{
			if (value != defaultValue)
			{
				descriptor.AddProperty(name, value);
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000034F4 File Offset: 0x000016F4
		public static void AddIDReferenceProperty(this ScriptComponentDescriptor descriptor, string name, string value, IControlResolver controlResolver)
		{
			if (!string.IsNullOrEmpty(value))
			{
				Control control = null;
				if (controlResolver != null)
				{
					control = controlResolver.ResolveControl(value);
				}
				if (control != null)
				{
					value = control.ClientID;
				}
				descriptor.AddProperty(name, value);
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003529 File Offset: 0x00001729
		public static void AddUrlProperty(this ScriptComponentDescriptor descriptor, string name, string value, IUrlResolutionService urlResolver)
		{
			if (!string.IsNullOrEmpty(value))
			{
				if (urlResolver != null)
				{
					value = urlResolver.ResolveClientUrl(value);
				}
				descriptor.AddProperty(name, value);
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003547 File Offset: 0x00001747
		public static void AddEvent(this ScriptComponentDescriptor descriptor, string name, string value, bool skipWithEmptyOrNullValue)
		{
			if (skipWithEmptyOrNullValue && string.IsNullOrEmpty(value))
			{
				return;
			}
			descriptor.AddEvent(name, value);
		}
	}
}
