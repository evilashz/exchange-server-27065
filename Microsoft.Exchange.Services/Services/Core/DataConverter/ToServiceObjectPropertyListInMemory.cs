using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001BA RID: 442
	internal class ToServiceObjectPropertyListInMemory : ToServiceObjectPropertyList
	{
		// Token: 0x06000C13 RID: 3091 RVA: 0x0003CE97 File Offset: 0x0003B097
		public ToServiceObjectPropertyListInMemory(Shape shape, ResponseShape responseShape) : base(shape, responseShape, StaticParticipantResolver.DefaultInstance)
		{
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x0003CEA6 File Offset: 0x0003B0A6
		protected override bool IsErrorReturnedForInvalidBaseShapeProperty
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x0003CEA9 File Offset: 0x0003B0A9
		protected override bool IsPropertyRequiredInShape
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0003CEAC File Offset: 0x0003B0AC
		public ServiceObject ConvertStoreObjectPropertiesToServiceObject(StoreObject storeObject, ServiceObject serviceObject)
		{
			return base.ConvertStoreObjectPropertiesToServiceObject(null, storeObject, serviceObject);
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0003CF70 File Offset: 0x0003B170
		protected override void ConvertPropertyCommandToServiceObject(IToServiceObjectCommand propertyCommand)
		{
			PropertyCommand.ToServiceObjectInMemoryOnly(delegate
			{
				try
				{
					GrayException.MapAndReportGrayExceptions(delegate()
					{
						PropertyCommand propertyCommand2 = propertyCommand as PropertyCommand;
						if (propertyCommand2 != null && propertyCommand2.ToServiceObjectRequiresMailboxAccess)
						{
							return;
						}
						this.<>n__FabricatedMethod5(propertyCommand);
					});
				}
				catch (GrayException ex)
				{
					if (ExTraceGlobals.ServiceCommandBaseDataTracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						ExTraceGlobals.ServiceCommandBaseDataTracer.TraceError<string, string>((long)this.GetHashCode(), "[ToServiceObjectPropertyListInMemory::ConvertStoreObjectPropertiesToServiceObject] Encountered PropertyRequestFailedException.  Exception: '{0}'. \n Property: {1} IgnoreCorruptPropertiesWhenRendering is true, so processing will continue.", ex.ToString(), propertyCommand.GetType().Name);
					}
				}
			});
		}
	}
}
