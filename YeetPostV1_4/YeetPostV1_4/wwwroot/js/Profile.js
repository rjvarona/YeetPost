new Vue({
    el: '#app',
    vuetify: new Vuetify(),
    data() {
        const defaultForm = Object.freeze({
            terms: false,
        })

        return {
            model: model,
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
        likeYeet: function (yeetID, whoLikes, location, remove) {


            index = this.model.yeets.findIndex(x => x.yeetID === yeetID);

            if (remove) {
                this.model.yeets[index].iLiked = false;
                this.model.yeets[index].totalLikes = parseInt(this.model.yeets[index].totalLikes) - 1;
            }
            else {
                this.model.yeets[index].iLiked = true;
                this.model.yeets[index].totalLikes = parseInt(this.model.yeets[index].totalLikes) + 1;
            }

            $.ajax({
                type: 'GET',
                url: '/Like/LikePost',
                traditional: true,
                data: {
                    yeetID: yeetID,
                    whoLikes: whoLikes,
                    location: location,
                    remove: remove,
                    from: "profile"
                },
                success: function (data) {

                },
                error: function (data) {
                    console.log('Error, please report to a developer')
                }
            }).done(data => {
                //this.model = JSON.parse(data);

            });

        },

        viewYeet: function (yeetID) {

            var url = '/Comment/ViewComments?yeetID=__yeetID__';
            window.location.href = url.replace('__yeetID__', yeetID);


        },

        deleteYeet: function (yeetID, location) {

            $.ajax({
                type: 'GET',
                url: '/Yeet/deleteYeet',
                traditional: true,
                data: {
                    yeetID: yeetID,
                    location: location,
                    from: "profile",

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

        changeModel: function (data) {

            this.model = data

            var x = this.model

        },


    },




})