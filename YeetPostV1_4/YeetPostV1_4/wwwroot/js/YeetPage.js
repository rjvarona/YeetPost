new Vue({
    el: '#app',
    vuetify: new Vuetify(),
    data() {
        const defaultForm = Object.freeze({
            terms: false,
        })

        return {
            header: '',
            dialog: false,
            dialog2: false,
            model: model,
            select: '',
            filteredModel: Object,
            inappropriate: 'innapropriate content',
            abusive: 'abusive',
            locationChosen: '',
            file:null,
            yeet: '',
            location: [
                { text: 'chattanooga' },
                { text: 'charlotte' },
                { text: 'cleveland' },
                { text: 'chicago' },
                { text: 'cincinnati' },
                { text: 'corona' }
            ],
            form: Object.assign({}, defaultForm),
            rules: {
                age: [
                    val => val < 10 || `I don't believe you!`,
                ],
                animal: [val => (val || '').length > 0 || 'This field is required'],
                name: [val => (val || '').length > 0 || 'This field is required'],
            },
           
            terms: false,
         
        }
    },

    computed: {
        formIsValid() {
            return (
                this.form.first &&
                this.form.last &&
                this.form.favoriteAnimal &&
                this.form.terms
            )
        },
    },

    methods: {
        resetForm() {
            this.form = Object.assign({}, this.defaultForm);
            this.$refs.form.reset();
        },
        submit() {
            
            this.resetForm();
            var x = $("#header").val();
            x = $('#yeet').val();

            if ($("#header").val() === "" || $("#yeet").val() === "") {
                return;
            }
            var y = this.file;

            var e = this;
            $.ajax({
                type: 'POST',
                url: '/Yeet/pushNewYeet',
                data: {
                    header: $("#header").val(),
                    yeet: $("#yeet").val(),
                    location: e.model.location
                },
                success: function (data) {
               
                },
                error: function (data) {
                    console.log('Error, please report to a developer')
                }
            }).done(data => {
                this.model = JSON.parse(data);

            });
            
            //location.reload(true);
        },
        //onchanging
        onchange(e) {

            var files = e.target.files || e.dataTransfer.files;
            if (!files.length)
                return;
            this.createImage(files[0]);
        },


        filterBy: function (recency, location, byLocation) {

            //this.isFiltered = true;
            if (byLocation) {
                this.model.location = location
            }
            else {
                this.model.location = location
            }

            var e = this
            this.resetForm()
            var x = $("#header").val()
            
            if (recency === null) {
                return;
            }


            $.ajax({
                type: 'GET',
                url: '/Yeet/filterBy',
                data: {
                    location: e.model.location,
                    byWhat: recency,
                },
                success: function (data) {
                  
                },
                error: function (data) {
                    console.log('Error, please report to a developer')
                }
            }).done(data => {
                this.model = JSON.parse(data);
              
            });

        },




        likeYeet: function (yeetID, whoLikes, location, remove)
        {
            var status = this.model.status;
            index = this.model.yeets.findIndex(x => x.yeetID === yeetID);

        
            $.ajax({
                type: 'GET',
                url: '/Like/LikePost',
                traditional: true,
                data: {
                    yeetID: yeetID,
                    whoLikes: whoLikes,
                    location: location,
                    status: status,
                    remove: remove,
                },
                success: function (data) {

                },
                error: function (data) {
                    console.log('Error, please report to a developer')
                }
            }).done(data => {

                var x = JSON.parse(data);
                this.model.yeets[index].iLiked = x.iLiked;
                this.model.yeets[index].totalLikes = x.totalLikes;
                this.model.yeets[index].whoLikes = x.whoLikes;

            });

        },


        //future put this in an object.
        flagYeet: function (yeetID, whoFlags, remove, reason, location) {

            var status = this.model.status;

            $.ajax({
                type: 'GET',
                url: '/Flag/FlagPost',
                traditional: true,
                data: {
                    yeetID: yeetID,
                    whoFlags: whoFlags,
                    location: location,
                    reason: reason,
                    remove: remove,
                    status: status,
                },
                success: function (data) {

                },
                error: function (data) {
                    console.log('Error, please report to a developer')
                }
            }).done(data => {
                this.model = JSON.parse(data);
                
                this.dialog = false;
                this.dialog2 = false;
            });


        },

        deleteYeet: function (yeetID, location) {

           
            var status = model.status;

            $.ajax({
                type: 'GET',
                url: '/Yeet/deleteYeet',
                traditional: true,
                data: {
                    yeetID: yeetID,
                    location: location,
                    status: status,
                   
                },
                success: function (data) {

                },
                error: function (data) {
                    console.log('Error, please report to a developer')
                }
            }).done(data => {
                this.model = JSON.parse(data);
                this.dialog = false;
                this.dialog2 = false;
            });
            return;


        },

        viewYeet: function (yeetID) {

            var url = '/Comment/ViewComments?yeetID=__yeetID__';
            window.location.href = url.replace('__yeetID__', yeetID);

    
        },

        changeModel: function (data) {

            this.model = data

            var x = this.model
            
        },
       
       
    },
   
   


})