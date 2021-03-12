using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001B8 RID: 440
	internal class ToServiceObjectPropertyList : ToServiceObjectPropertyListBase
	{
		// Token: 0x06000BFF RID: 3071 RVA: 0x0003CBD0 File Offset: 0x0003ADD0
		private ToServiceObjectPropertyList()
		{
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0003CBD8 File Offset: 0x0003ADD8
		public ToServiceObjectPropertyList(Shape shape, ResponseShape responseShape, IParticipantResolver participantResolver) : base(shape, responseShape)
		{
			this.participantResolver = participantResolver;
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x0003CBE9 File Offset: 0x0003ADE9
		// (set) Token: 0x06000C02 RID: 3074 RVA: 0x0003CBF1 File Offset: 0x0003ADF1
		public char[] CharBuffer { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x0003CBFA File Offset: 0x0003ADFA
		// (set) Token: 0x06000C04 RID: 3076 RVA: 0x0003CC02 File Offset: 0x0003AE02
		public CommandOptions CommandOptions { get; set; }

		// Token: 0x06000C05 RID: 3077 RVA: 0x0003CC0B File Offset: 0x0003AE0B
		protected override ToServiceObjectCommandSettingsBase GetCommandSettings()
		{
			return new ToServiceObjectCommandSettings();
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0003CC12 File Offset: 0x0003AE12
		protected override ToServiceObjectCommandSettingsBase GetCommandSettings(PropertyPath propertyPath)
		{
			return new ToServiceObjectCommandSettings(propertyPath);
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0003CC1C File Offset: 0x0003AE1C
		protected override bool ValidateProperty(PropertyInformation propertyInformation, bool returnErrorForInvalidProperty)
		{
			bool implementsToServiceObjectCommand = propertyInformation.ImplementsToServiceObjectCommand;
			if (!implementsToServiceObjectCommand && returnErrorForInvalidProperty)
			{
				throw new InvalidPropertyForOperationException(propertyInformation.PropertyPath);
			}
			return implementsToServiceObjectCommand;
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x0003CC43 File Offset: 0x0003AE43
		// (set) Token: 0x06000C09 RID: 3081 RVA: 0x0003CC4B File Offset: 0x0003AE4B
		internal bool IgnoreCorruptPropertiesWhenRendering { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x0003CC54 File Offset: 0x0003AE54
		private IParticipantResolver ParticipantResolver
		{
			get
			{
				if (this.participantResolver == null)
				{
					this.participantResolver = StaticParticipantResolver.DefaultInstance;
				}
				return this.participantResolver;
			}
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0003CC70 File Offset: 0x0003AE70
		public IList<IToServiceObjectCommand> CreatePropertyCommands(IdAndSession idAndSession, StoreObject storeObject, ServiceObject serviceObject)
		{
			List<IToServiceObjectCommand> list = new List<IToServiceObjectCommand>();
			foreach (CommandContext commandContext in this.commandContextsOrdered)
			{
				ToServiceObjectCommandSettings toServiceObjectCommandSettings = (ToServiceObjectCommandSettings)commandContext.CommandSettings;
				toServiceObjectCommandSettings.IdAndSession = idAndSession;
				toServiceObjectCommandSettings.StoreObject = storeObject;
				toServiceObjectCommandSettings.ServiceObject = serviceObject;
				toServiceObjectCommandSettings.ResponseShape = this.responseShape;
				toServiceObjectCommandSettings.CommandOptions = this.CommandOptions;
				list.Add((IToServiceObjectCommand)commandContext.PropertyInformation.CreatePropertyCommand(commandContext));
			}
			return list;
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0003CD18 File Offset: 0x0003AF18
		private void ConvertStoreObjectPropertiesToServiceObject(ServiceObject serviceObject, IdAndSession idAndSession, StoreObject storeObject)
		{
			IList<IToServiceObjectCommand> list = this.CreatePropertyCommands(idAndSession, storeObject, serviceObject);
			List<Participant> list2 = new List<Participant>();
			foreach (IToServiceObjectCommand toServiceObjectCommand in list)
			{
				IPregatherParticipants pregatherParticipants = toServiceObjectCommand as IPregatherParticipants;
				if (pregatherParticipants != null)
				{
					pregatherParticipants.Pregather(storeObject, list2);
				}
			}
			this.ParticipantResolver.LoadAdDataIfNeeded(list2);
			foreach (IToServiceObjectCommand toServiceObjectCommand2 in list)
			{
				IRequireCharBuffer requireCharBuffer = toServiceObjectCommand2 as IRequireCharBuffer;
				if (requireCharBuffer != null)
				{
					requireCharBuffer.CharBuffer = this.CharBuffer;
				}
				this.ConvertPropertyCommandToServiceObject(toServiceObjectCommand2);
			}
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0003CDE4 File Offset: 0x0003AFE4
		public ServiceObject ConvertStoreObjectPropertiesToServiceObject(IdAndSession idAndSession, StoreObject storeObject, ServiceObject serviceObject)
		{
			this.ConvertStoreObjectPropertiesToServiceObject(serviceObject, idAndSession, storeObject);
			return serviceObject;
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0003CDF0 File Offset: 0x0003AFF0
		protected virtual void ConvertPropertyCommandToServiceObject(IToServiceObjectCommand propertyCommand)
		{
			try
			{
				propertyCommand.ToServiceObject();
			}
			catch (PropertyRequestFailedException ex)
			{
				if (!this.IgnoreCorruptPropertiesWhenRendering)
				{
					throw;
				}
				if (ExTraceGlobals.ServiceCommandBaseDataTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					ExTraceGlobals.ServiceCommandBaseDataTracer.TraceError<string, string>((long)this.GetHashCode(), "[ToServiceObjectPropertyList::ConvertStoreObjectPropertiesToServiceObject] Encountered PropertyRequestFailedException.  Message: '{0}'. Data: {1} IgnoreCorruptPropertiesWhenRendering is true, so processing will continue.", ex.Message, ((IProvidePropertyPaths)ex).GetPropertyPathsString());
				}
			}
		}

		// Token: 0x04000970 RID: 2416
		public static ToServiceObjectPropertyList Empty = new ToServiceObjectPropertyList();

		// Token: 0x04000971 RID: 2417
		private IParticipantResolver participantResolver;
	}
}
