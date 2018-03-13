function resultFormatter(umbRequestHelper) {
    
    function configureResult(content, treeAlias, appAlias) {
        content.menuUrl = umbRequestHelper.getApiUrl("contentTreeBaseUrl", "GetMenu", [{ id: content.id }, { application: appAlias }]);
        content.editorPath = appAlias + "/" + treeAlias + "/edit/" + content.id;
        angular.extend(content.metaData, { treeAlias: treeAlias });
        content.subTitle = content.alias;
    }

    return {
        configureResult: configureResult
    };
}

angular.module('umbraco.services').factory('customResultFormatter', resultFormatter);