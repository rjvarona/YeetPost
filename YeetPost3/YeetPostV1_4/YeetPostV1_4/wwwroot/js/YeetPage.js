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
            model: model,
            select: '',
            filteredModel: Object,
            inappropriate: 'innapropriate content',
            abusive: 'abusive',
            locationChosen: '',
            yeet: '',
            location: [
                { text: 'chicago' },
                { text: 'chattanooga' }
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
            this.form = Object.assign({}, this.defaultForm)
            this.$refs.form.reset()
        },
        submit() {
            
            this.resetForm()
            var x = $("#header").val()

            if ($("#header").val() === null || $("#yeet").val() === null) {
                return;
            }
            var e = this
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
        filterBy: function (recency, location, byLocation) {

            //this.isFiltered = true;
            if (byLocation) {
                this.model.location = location.text
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
            
            $.ajax({
                type: 'GET',
                url: '/Like/LikePost',
                traditional: true,
                data: {
                    yeetID: yeetID,
                    whoLikes: whoLikes,
                    location: location,
                    remove: remove,
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

        flagYeet: function (yeetID, whoFlags, remove, reason ,location) {

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