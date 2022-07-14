function onBeforeRender(dashboardControl) {
	var viewerApi = dashboardControl.findExtension('viewerApi');
	if (viewerApi) {
		viewerApi.on('itemMasterFilterStateChanged', e => onItemMasterFilterStateChanged(dashboardControl, e));
	}
}

function onItemMasterFilterStateChanged(dashboardControl, e) {
	if (e.itemName === 'gridDashboardItem1') {
		var viewerApi = dashboardControl.findExtension('viewerApi');
		var filterValues = viewerApi.getCurrentFilterValues(e.itemName);
		if (filterValues) {
			var slice = viewerApi.getItemData(e.itemName).getSlice(filterValues[0].getAxisPoint());
			var productIdMeasure = slice.getMeasures().filter(m => m.dataMember === 'ProductID')[0];
			var productId = slice.getMeasureValue(productIdMeasure.id).getValue();

			// Send a ProductId value to the server using Request Headers
			dashboardControl.remoteService.headers = { 'ProductId': productId };
			// Refresh the Chart Dashboard Item
			dashboardControl.refresh(['chartDashboardItem1']);
		}
	}
}