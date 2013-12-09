/// <reference path="../../jasmine.js"/>
/// <reference path="../../angular.js"/>
/// <reference path="../../angular-mocks.js"/>
/// <reference path="../../../../BauerDevSession.AngularJS/Scripts/apps/gift-request/gift-request.js" />


describe("GifeRequestService", function() {

    var $httpBackend, giftRequestService, gifts;

    beforeEach(function() {
        module('giftRequest');

        inject(function(GifeRequestService, _$httpBackend_) {
            giftRequestService = GifeRequestService;
            $httpBackend = _$httpBackend_;

            $httpBackend.when("GET", "/Default/GetListOfGifts").respond([{ "Code": "PS4", "Name": "Play Station 4" }, { "Code": "X1", "Name": "XBox One" }]);
        });
        gifts = [];
    });

    it("Make sure get request is made", function() {
        $httpBackend.expectGET("/Default/GetListOfGifts");

        giftRequestService.getListOfGifts();

        $httpBackend.flush();
    });

    afterEach(function () {
        $httpBackend.verifyNoOutstandingExpectation();
        $httpBackend.verifyNoOutstandingRequest();
    });
});


describe("GiftRequestController", function () {
    var $scope, $httpBackend, giftRequestService;

    beforeEach(function() {
        module("giftRequest");

        inject(function (GifeRequestService, _$httpBackend_) {
            giftRequestService = GifeRequestService;
            $httpBackend = _$httpBackend_;

            $httpBackend.when("GET", "/Default/GetListOfGifts").respond([{ "Code": "PS4", "Name": "Play Station 4" }, { "Code": "X1", "Name": "XBox One" }]);
        });

        inject(function($rootScope, $controller) {
            $scope = $rootScope.$new();
            $controller("GiftRequestController", { $scope: $scope, giftRequestService: giftRequestService });
        });

    });

    it("submit request should create selected gift when gift request submitted", function () {

        $scope.gift = { "Code": "PS4", "Name": "Play Station 4" };
        $scope.firstName = "Mr";
        $scope.lastName = "Angular";

        $scope.submitGiftRequest();

        expect($scope.selectedGift).not.toBeNull();
    });

});